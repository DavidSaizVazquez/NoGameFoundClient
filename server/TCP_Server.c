//
// Created by tania on 30/10/20.
//


#include "TCP_Server.h"





void *connection_handler(void *arg)
{
    int stop = 0;
    Args *args = (Args *)arg;
    int sock_conn = args->sock;
    int pos = args->pos;
    int ret;
    char petition[512] = {};
    char answer[512] = {};
    MYSQL *conn = args->conn;
    char user[20] = {};
    char password[20] = {};
    int age = 33;
    char mail[20] = {};
    int spam;
    int i = 0;

    while (stop == 0) {
        ret = read(sock_conn, petition, sizeof(petition));
        if (ret < 0) {
            printf("no data in buffer\n");
        } else {
            printf("received\n");
            petition[ret] = '\0';
            printf("Petition: %s\n", petition);
            char *p = strtok(petition, "/");
            if(p==NULL){
                printf("not formated message, closing\n");
                stop=1;
                break;
            }
            char data[512] = {};
            strcpy(data, p + 2);
            int code = (int) strtol(p, (char **) NULL, 10);
            switch (code) {
                case 0:
                    stop = 1;
                    break;
                case 1:
                    //LOGIN 1/userName,password -->1/0 good 1/-1 NG
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(password, p);
                    pthread_mutex_lock(args->mutex);
                    sprintf(answer, "1/%d", loginUser(conn, user, password));
                    strcpy(args->userList->list[pos].userName, user);
                    pthread_mutex_unlock(args->mutex);
                    break;
                case 2:
                    //REGISTER 1/userName,password,age,mail,spam -->1/0 good 1/-1 not good
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(password, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)age = (int) strtol(p, (char **) NULL, 10);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(mail, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)spam = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(args->mutex);
                    sprintf(answer, "2/%d", addUser(conn, user, password, age, mail, spam));
                    pthread_mutex_unlock(args->mutex);
                    break;
                case 3:
                    //GET AGE 2/userName -->2/age
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(args->mutex);
                    getAge(conn, user, &age);
                    pthread_mutex_unlock(args->mutex);
                    sprintf(answer, "3/%d", age);
                    break;

                case 4:
                    //GET MAIL 3/userName -->3/mail
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(args->mutex);
                    getEmail(conn, user, mail);
                    pthread_mutex_unlock(args->mutex);
                    sprintf(answer, "4/%s", mail);
                    break;
                case 5:
                    //CHANGE SPAM 4/userName,newspam -->4/0 changed 4/-1 not changed
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)spam = (int) strtol(p, (char **) NULL, 10);
                    pthread_mutex_lock(args->mutex);
                    sprintf(answer, "5/%d", setSpam(conn, user, spam));
                    pthread_mutex_unlock(args->mutex);
                    break;
                case 6:
                    //GET SPAM 6/userName-->6/1(true) else 6/0
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    pthread_mutex_lock(args->mutex);
                    getSpam(conn, user, &spam);
                    pthread_mutex_unlock(args->mutex);
                    sprintf(answer, "6/%d", spam);
                    break;
                case 7:
                    //GET USER LIST -->7/0 returns 7/user1,user2,user3,...,
                    strcpy(answer,"7/");
                    pthread_mutex_lock(args->mutex);
                    for(i=0;i<args->userList->num;i++){
                        strcat(answer,args->userList->list[i].userName);
                        strcat(answer,",");
                    }
                    pthread_mutex_unlock(args->mutex);
                    break;
                default:
                    //ERROR CODE
                    sprintf(answer, "-1/%d", code);
            }
            if (code != 0) {
                printf("Answer: %s\n", answer);
                // Send aswer
                write(sock_conn, answer, strlen(answer));
            }
        }
    }
    close(sock_conn);
    return NULL;
}

int startTCPServer(struct sockaddr_in *serv_adr, int *sock_listen, int PORT) {
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
    if (bind(*sock_listen, (struct sockaddr *) serv_adr, sizeof(*serv_adr)) != 0) {
        printf("Error binding\n");
        return -1;
    }
    printf("binding ended\n");


    return 0;
}
