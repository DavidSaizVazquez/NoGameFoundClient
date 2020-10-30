//
// Created by tania on 30/10/20.
//
#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include "TCP_Server.h"
#include "SQL_Conn.h"

void *connection_handler(void *arg)
{


    int stop = 0;
    int sock_conn = *((int*)arg);
    int ret;
    char petition[512] = {};
    char answer[512] = {};
    MYSQL *conn = NULL;
    char user[20] = {};
    char password[20] = {};
    int age = 33;
    char mail[20] = {};
    int spam;

    while (stop == 0) {
        ret = read(sock_conn, petition, sizeof(petition));
        if (ret < 0) {
            printf("no data in buffer\n");
        } else {
            printf("received\n");
            petition[ret] = '\0';
            printf("Petition: %s\n", petition);
            char *p = strtok(petition, "/");
            char data[512] = {};
            strcpy(data, p + 2);
            int code = (int) strtol(p, (char **) NULL, 10);
            switch (code) {
                case 0:
                    stop = 1;
                    break;
                case 1:
                    //LOGIN 1/user,password -->1/0 good 1/-1 NG
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)strcpy(password, p);
                    sprintf(answer, "1/%d", loginUser(conn, user, password));
                    break;
                case 2:
                    //REGISTER 1/user,password,age,mail,spam -->1/0 good 1/-1 not good
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
                    sprintf(answer, "2/%d", addUser(conn, user, password, age, mail, spam));
                    break;
                case 3:
                    //GET AGE 2/user -->2/age
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    getAge(conn, user, &age);
                    sprintf(answer, "3/%d", age);
                    break;

                case 4:
                    //GET MAIL 3/user -->3/mail
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    getEmail(conn, user, mail);
                    sprintf(answer, "4/%s", mail);
                    break;
                case 5:
                    //CHANGE SPAM 4/user,newspam -->4/0 changed 4/-1 not changed
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if (p != NULL)spam = (int) strtol(p, (char **) NULL, 10);
                    sprintf(answer, "5/%d", setSpam(conn, user, spam));
                    break;
                case 6:
                    //GET SPAM 6/user-->6/1(true) else 6/0
                    p = strtok(data, ",");
                    if (p != NULL)strcpy(user, p);
                    getSpam(conn, user, &spam);
                    sprintf(answer, "6/%d", spam);
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
    return 0;
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
