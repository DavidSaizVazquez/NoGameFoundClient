//
// Created by tania on 30/10/20.
//


#include "TCP_Server.h"

extern MYSQL * conn;
extern UserList userList;
extern pthread_mutex_t mutex;




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
    int i = 0;
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
            printf("received\n");
            petition[ret] = '\0';
            printf("Petition: %s\n", petition);
            char *p = strtok(petition, "/");
            if (p == NULL) {
                printf("not formated message, closing\n");
                stop = 1;
                break;
            }
            int code = (int) strtol(p, (char **) NULL, 10);

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
                    catUsers(&userList,answer);
                    strcat(answer,"~");
                    pthread_mutex_unlock(&mutex);
                    break;
                case 8:
                    // CREATE GAME 8/user,game
                    pthread_mutex_lock(&mutex);
                    if(createGame(conn, (char *) userList.list[pos].userName, &game) == -1)game=-1;
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
                    i=sendInvitation(user,sendingUser,game);
                    sprintf(answer, "9/%d~",i);
                    pthread_mutex_unlock(&mutex);
                    break;
                case 10:
                    game = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(&mutex);
                    if(game!=-1)joinGame(conn, (char *) userList.list[pos].userName, game);
                    refreshGameFlag=1;
                    pthread_mutex_unlock(&mutex);
                    break;
                case 12: //inici partida
                    p = strtok(NULL,",");
                    int gameNumber = (int) strtol(p, (char **) NULL, 10);
                
                    pthread_mutex_lock(&mutex);
                    usersFromGame(conn, &players, gameNumber);
                    strcpy(answer,"12/0~");
                    for(int k=0; k < players.num; k++){
                        printf("Sending: %s to %s", answer, players.list[k].userName);
                        write(players.list[k].socket, answer, strlen(answer));
                    }
                    
                    pthread_mutex_unlock(&mutex);

                    break;
                case 22:
                    p = strtok(NULL, "/");
                    position.x=(float ) strtof(p, (char **) NULL);
                    p = strtok(NULL, "/");
                    position.y=(float ) strtof(p, (char **) NULL);
                    printf("%f,%f\n",position.x,position.y);
                    break;
                default:
                    //ERROR CODE
                    sprintf(answer, "-1/%d~", code);
            }
            if (code != 0 && code!=10) {
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
                    catUsers(&userList,answer);
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
                usersFromGame(conn, &tempList, game);
                catUsers(&tempList,answer);
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
    close(sock_conn);
    return NULL;
}

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

int removeUser(UserList* list, int pos){
    for(int i=pos; i<(list->num - 1); i++){
        list->list[i] = list->list[i + 1];
    }
    list->num= list->num - 1;
    return 0;
}

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
