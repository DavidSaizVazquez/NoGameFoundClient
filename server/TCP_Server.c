//
// Created by tania on 30/10/20.
//

#include "TCP_Server.h"
extern MYSQL * conn;
extern UserList userList;
extern pthread_mutex_t mutex;
extern int i;



/***
 * Connection handler is the function executed in a thread to regulate the server access of a given channel
 * @param arg: position in the userList global object.
 * @return void
 */
void *connection_handler(void *arg)
{
    int stop = 0;
    int pos =*(int*)arg;
    int sock_conn = userList.list[pos].socket;
    int ret;
    char petition[512] = {};
    char answer[512] = {};
    char user[20] = {};
    char sendingUser[20] = {};
    char password[20] = {};
    int age = 33;
    char mail[20] = {};
    int spam;
    int game;
    int l = 0;
    int login=0;
    Position position={};
    UserList players={};


    UserName userNames[NUM_CLIENT] = {};
    int refreshGameFlag=0;

    while (stop == 0) {
        ret = read(sock_conn, petition, sizeof(petition));
        if (ret < 0) {
            printf("no data in buffer\n");
        } else {
            petition[ret] = '\0';
            //printf("Petition: %s\n", petition);
            char *p = strtok(petition, "/");
            if (p == NULL) {
                printf("not formated message, closing\n");
                stop = 1;
                break;
            }
            int code = (int) strtol(p, (char **) NULL, 10);
            char broad[512] ={};
            switch (code) {
                case 0:
                    stop = 1;
                    break;
                case 1:
                    //LOGIN 1/userName,password -->1/0 good 1/-1 NG
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(password, p);
                    pthread_mutex_lock(&mutex);
                    if(findUser(&userList,user)==-1)login = loginUser(conn, user, password);
                    else login=-1;
                    sprintf(answer, "1/%d~", login);
                    if (login == 0) {
                        strcpy((char *) userList.list[pos].userName, user);
                        userList.num++;
                        sendAllStartingGames();
                    }

                    pthread_mutex_unlock(&mutex);

                    break;
                case 2:
                    //REGISTER 2/userName,password,age,mail,spam -->1/0 good 1/-1 not good
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(password, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)age = (int) strtol(p, (char **) NULL, 10);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(mail, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)spam = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(&mutex);
                    sprintf(answer, "2/%d~", addUser(conn, user, password, age, mail, spam));
                    pthread_mutex_unlock(&mutex);
                    break;
                case 3:
                    //GET AGE 3/userName -->2/age
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(&mutex);
                    getAge(conn, user, &age);
                    pthread_mutex_unlock(&mutex);
                    sprintf(answer, "3/%d~", age);
                    break;

                case 4:
                    //GET MAIL 4/userName -->3/mail
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(&mutex);
                    getEmail(conn, user, mail);
                    pthread_mutex_unlock(&mutex);
                    sprintf(answer, "4/%s~", mail);
                    break;
                case 5:
                    //CHANGE SPAM 5/userName,newspam -->4/0 changed 4/-1 not changed
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)spam = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(&mutex);
                    sprintf(answer, "5/%d~", setSpam(conn, user, spam));
                    pthread_mutex_unlock(&mutex);
                    break;
                case 6:
                    //GET SPAM 6/userName-->6/1(true) else 6/0
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(&mutex);
                    getSpam(conn, user, &spam);
                    pthread_mutex_unlock(&mutex);
                    sprintf(answer, "6/%d~", spam);
                    break;
                case 7:
                    //GET USER LIST -->7/0 returns 7/user1,user2,user3,...,
                    strcpy(answer, "7/");
                    pthread_mutex_lock(&mutex);
                    catUsers(&userList,answer,",");
                    strcat(answer,"~");
                    pthread_mutex_unlock(&mutex);
                    break;
                case 8:
                    // CREATE GAME 8/user,game
                    pthread_mutex_lock(&mutex);
                    if(createGame(conn, (char *) userList.list[pos].userName, &game) == -1)game=-1;
                    sendAllStartingGames();
                    pthread_mutex_unlock(&mutex);
                    refreshGameFlag = 1;
                    sprintf(answer, "8/%d~", game);

                    break;

                case 9:
                    //INVITE TO GAME 9/user,game
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(sendingUser, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)game = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(&mutex);
                    l=sendInvitation(user, sendingUser, game);
                    sprintf(answer, "9/%d~", l);
                    pthread_mutex_unlock(&mutex);
                    break;

                case 10:
                    p = strtok(NULL, "\0");
                    game = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(&mutex);
                    if(game!=-1)joinGame(conn, (char *) userList.list[pos].userName, game);
                    refreshGameFlag=1;
                    pthread_mutex_unlock(&mutex);
                    break;

                case 12: //inici partida
                    p = strtok(NULL,",");
                    int gameNumber = (int) strtol(p, (char **) NULL, 10);
                    p = strtok(NULL,",");
                    pthread_mutex_lock(&mutex);
                    usersFromGame(conn, &players, &userList,gameNumber);

                    for(int k=0; k < players.num; k++){
                        sprintf(broad,"12/0,%s,%d~",p,k+1);//TODO change to k
                        printf("Sending: %s to %s\n", broad, players.list[k].userName);
                        write(players.list[k].socket, broad, strlen(broad));
                    }
                    catUsers(&players,broad,"*");
                    startGame(conn,broad,gameNumber);
                    pthread_mutex_unlock(&mutex);
                    break;


                case 13:
                    //UPDATE LIST 13/--> no return
                    pthread_mutex_lock(&mutex);
                    usersFromGame(conn, &players, &userList,game);
                    pthread_mutex_unlock(&mutex);
                    break;

                case 14:
                    //JOIN GAME 14/game
                    p = strtok(NULL, ",");

                    game = (int) strtol(p, (char **) NULL, 10);

                    if(game!=-1){
                        pthread_mutex_lock(&mutex);
                        joinGame(conn, (char *) userList.list[pos].userName, game);
                        pthread_mutex_unlock(&mutex);
                        sprintf(answer, "14/0~");
                        refreshGameFlag=1;
                    } else{
                        sprintf(answer, "14/-1~");
                    }
                    break;

                case 15: //exit game (15/0)

                    if(exitGame(conn,user,game)==0){
                        sprintf(answer, "15/0~");
                    } else {
                        sprintf(answer, "15/-1~");
                    }

                    pthread_mutex_lock(&mutex);
                    usersFromGame(conn, &players, &userList,gameNumber);
                    pthread_mutex_unlock(&mutex);
                    refreshGameFlag = 1;


                    break;
                case 16: //scoreboard data
                    //received 16/0
                    //sent 16/id-player1*player2*-score,id-player2*player3*player4*-score,...~ to only one player
                    p = strtok(NULL, "\0");
                    pthread_mutex_lock(&mutex);
                    strcpy(answer,"16/");
                    if(finishedGames(conn,answer)<0)strcpy(answer,"-1");
                    strcat(answer,"~");
                    pthread_mutex_unlock(&mutex);
                    break;

                case 17:// 17/0
                    // 17/p1,p2......
                    pthread_mutex_lock(&mutex);
                    strcpy(answer,"17/");
                    if(playersPlayedWith(conn,answer,user)<0)strcpy(answer,"-1");
                    strcat(answer,"~");
                    pthread_mutex_unlock(&mutex);
                    break;

                case 18: //delete user profile
                    //received 18/0
                    //if success, respond 18/0
                    if(1){
                        int res = deleteUser(conn, user);
                        if(res==0){
                            strcpy(answer, "18/0~");
                        } else{
                            strcpy(answer, "18/-1~");
                        }
                    }
                    break;






                case 20: //received player's last position, state,...
                    //mgs format: 20/pos_x,pos_y,state,looking dir(-1,1,0)~
                    p=strtok(NULL,"~");
                    sprintf(broad,"20/%s/%s~",user,p);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }

                    }
                    sprintf(answer, "-1/%d~", code);
                    break;
                case 21: // generate bullet at position x
                // msg: 21/x,y,vx,vy
                    p=strtok(NULL,"~");
                    sprintf(broad,"21/%s/%s~",user,p);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }
                    }
                    break;
                case 22: //boss message
                    p=strtok(NULL, "~");
                    sprintf(broad, "22/%s~",p);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }
                    }
                    break;
                case 23:
                    p=strtok(NULL, "~");
                    sprintf(broad, "23/%s~",p);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            //printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }

                    }
                    sprintf(answer, "-1/%d~", code);
                    break;

                case 24: //boss died
                    p=strtok(NULL,"~");
                    sprintf(broad, "24/%s~",p);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }

                    }
                    sprintf(answer, "-1/%d~", code);
                    break;

                case 25://25/1,2,...
                    //send to all the same string
                    p=strtok(NULL, "~");
                    sprintf(broad, "25/%s~",p);
                    pthread_mutex_lock(&mutex);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }

                    }
                    pthread_mutex_unlock(&mutex);
                    sprintf(answer, "-1/%d~", code);
                    break;

                case 30:// revive call
                    //30/-->30/username
                    sprintf(broad,"30/%s/~",user);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }

                    }
                    break;
                case 33:// revive call
                    //30/-->30/username
                    sprintf(broad,"33/%s/~",user);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            //printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }
                    }
                    break;
                case 31:
                    p=strtok(NULL,"~");
                    sprintf(broad,"31/%s/%s~",user,p);
                    pthread_mutex_lock(&mutex);
                    for(int k=0; k < players.num; k++){
                        if(players.list[k].socket != sock_conn)
                        {
                            printf("Sending: %s to %s\n", broad, players.list[k].userName);
                            write(players.list[k].socket, broad, strlen(broad));
                        }
                    }
                    pthread_mutex_unlock(&mutex);
                    break;
                case 32:
                    p=strtok(NULL,"~");
                    printf("finished game with puntuation %s",p);
                    setGameScore(conn,(int) strtol(p, (char **) NULL, 10),game);
                    exitGame(conn,user,game);
                    break;
            }
            if (code != 0 && code!=10 && code!=12 && code!=13 &&code<19) {
                printf("Answer: %s\n", answer);
                // Send answer
                write(sock_conn, answer, strlen(answer));
            }
            if(code==0){
                
                //user is removed we update the list
                printf("closing user %s\n",(char *) userList.list[pos].userName);
                removeUser(&userList, pos);
            }
            //user was added or removed, we update the lists
            if(code==1 || code == 0){
                for(int j=0; j<userList.num;j++){
                    pthread_mutex_lock(&mutex);
                    strcpy(answer, "7/");
                    catUsers(&userList,answer,",");
                    strcat(answer,"~");
                    printf("sending %s to %s\n",answer,(char *)userList.list[j].userName);
                    write(userList.list[j].socket, answer, strlen(answer));
                    pthread_mutex_unlock(&mutex);
                }

            }
            if(refreshGameFlag==1){
                strcpy(answer,"11/");
                UserList tempList={};
                pthread_mutex_lock(&mutex);
                usersFromGame(conn, &tempList, &userList,game);
                catUsers(&tempList,answer,",");
                strcat(answer, "~");
                for(int j=0;j<tempList.num;j++){
                    int k=0;
                    int found=0;
                    while(k<userList.num&&found==0){
                        if(strcmp(userList.list[k].userName,tempList.list[j].userName)==0){
                            found=1;
                            printf("%s to %s on socket %d\n",answer, userList.list[k].userName,userList.list[k].socket);
                            write(userList.list[k].socket, answer, strlen(answer));
                        }
                        k++;
                    }
                }
                pthread_mutex_unlock(&mutex);
                refreshGameFlag=0;

            }
        }
    }
    exitGame(conn,user,game);
    close(sock_conn);
    i--;
    return NULL;
}

/***
 * Sends a message inviting the user to the game
 * @param user user to send the message
 * @param sendingUser user that sends the message
 * @param game game the player is being invited
 * @return -1 if not found 0 if found
 */
int sendInvitation(char user[20],char sendingUser[20] ,int game) {
    int i=0;
    u_int8_t found =0;
    char answer[520]={};
    while(i<userList.num && found==0){
        if(strcmp((const char *) userList.list[i].userName, sendingUser) == 0){
            found=1;
            printf("10/%s,%d\n",user,game);
            sprintf(answer,"10/%s,%d~",user,game);
            write(userList.list[i].socket, answer, strlen(answer));
            printf(answer,"10/%s,%d\n",user,game);
        }
        i++;
    }
    if(found==0){
        return -1;
    }
    return 0;
}
/***
 * sends to the user all possible games to play
 * @return 0, error handling not implemented in this function
 */
int sendAllStartingGames(){
    char msg[512] = {};
    strcpy(msg,"13/");
    ongoingGames(conn,msg);
    strcat(msg,"~");

    printf("Sending command 13: %s\n",msg);

    for(int i=0; i<userList.num; i++){
        write(userList.list[i].socket, msg, strlen(msg));
    }
    return 0;
}
/***
 * searches for a user and removes it from the list
 * @param list list to remove it from
 * @param pos postion of the removing item
 * @return 0 if it works
 */
int removeUser(UserList* list, int pos){
    for(int i=pos; i<(list->num); i++){
        list->list[i] = list->list[i + 1];
    }
    list->num= list->num - 1;
    return 0;
}
/***
 * Starts the TCP Server
 * @param serv_adr address of the server
 * @param sock_listen socket where the connection is created
 * @param PORT port of the connection
 * @return  -1 if any error happened, 0 elsewise
 */
int startTCPServer(struct sockaddr_in *serv_adr, int *sock_listen, int PORT) {
    int err;
    if ((*sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
        printf("Error creating socket\n");
        return -1;
    }
    printf("Socket created\n");


    memset(serv_adr, 0, sizeof(*serv_adr));// inicialitza a zero serv_addr
    serv_adr->sin_family = AF_INET;

    // asocia el socket a cualquiera de las IP de la maquina.
    //htonl formatea el numero que recibe al formato necesario
    serv_adr->sin_addr.s_addr = htonl(INADDR_ANY);

    // establecemos el puerto de escucha
    serv_adr->sin_port = htons(PORT);
    int one = 1;
    setsockopt(*sock_listen, SOL_SOCKET, SO_REUSEADDR, &one, sizeof(one));
    if ((err=bind(*sock_listen, (struct sockaddr *) serv_adr, sizeof(*serv_adr))) != 0) {
        printf("Error binding %d\n",err);
        return -1;
    }

    printf("binding ended\n");


    return 0;
}
