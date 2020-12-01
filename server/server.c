#include <stdlib.h>
#include <unistd.h>
#include <pthread.h>
#include <mysql.h>
#include "TCP_Server.h"
#include "SQL_Conn.h"
#include <stdio.h>
#include "mysql.h"

UserList userList={};
MYSQL *conn = NULL;
pthread_mutex_t mutex=PTHREAD_MUTEX_INITIALIZER;

int main()
{
    //config variables
    int PORT=9990;
    //mysql
    char* host="localhost";
    char* user="root";
    char* passw="";
    char * db="GameDB";


    int sock_conn, sock_listen;
    struct sockaddr_in serv_adr;
    char msg[20];


    if (initMySQLServer(&conn,host, user, passw, db) < 0) {
        printf("Error opening Mysql");
        exit(1);
    } else {
        printf("Mysql opened\n");
    }

    if(resetSQLTable(conn, "UsersPerGame") < 0){
        printf("Error cleaning db");
        exit(1);
    }
    if(resetSQLTable(conn, "Games") < 0){
        printf("Error cleaning db");
        exit(1);
    }

    if (startTCPServer(&serv_adr, &sock_listen, PORT) < 0) {
        printf("Error opening tcp socket");
        exit(1);
    } else {
        printf("connection secured\n");
    }
    // Server end

    int i;
    pthread_t threads[NUM_CLIENT];

    for (i=0; i<NUM_CLIENT; i++) {
        if (listen(sock_listen, 3) != 0) {
            printf("Error en el Listen\n");
            return -1;
        }
        printf("listening\n");
        sock_conn = accept(sock_listen, NULL, NULL);
        printf("accepted %d at socket: %d\n",i, sock_conn);
        pthread_mutex_lock(&mutex);
        userList.list[i].socket=sock_conn;
        if(pthread_create(&threads[i] , NULL , connection_handler , (void*) &i) != 0)
        {
            perror("could not create thread");
            return -1;
        }
        pthread_mutex_unlock(&mutex);
        sleep(2);
    }
    printf("closing");
    close(sock_conn);
    return 0;
}

