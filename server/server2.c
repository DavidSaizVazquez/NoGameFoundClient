#include <stdio.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdlib.h>
#include <unistd.h>
#include <pthread.h>
#include <mysql.h>
#include "TCP_Server.h"
#include "SQL_Conn.h"



int main()
{

    int sock_conn, sock_listen;
    struct sockaddr_in serv_adr;
    MYSQL *conn = NULL;


    if (initMySQLServer(&conn) < 0) {
        printf("Error opening Mysql");
        exit(1);
    } else {
        printf("Mysql opened");
    }

    if (startTCPServer(&serv_adr, &sock_listen, 13555) < 0) {
        printf("Error opening tcp socket");
        exit(1);
    } else {
        printf("connection secured");
    }
    // Server end

    int i;
    pthread_t threads[NUM_CLIENT];

    for (i=1; i<=NUM_CLIENT; i++) {
        if (listen(sock_listen, 3) != 0) {
            printf("Error en el Listen\n");
            return -1;
        }
        printf("listening\n");
        sock_conn = accept(sock_listen, NULL, NULL);
        printf("accepted at socket: %d\n", sock_conn);

        if(pthread_create(&threads[i] , NULL , connection_handler , (void*) &sock_conn) != 0)
        {
            perror("could not create thread");
            return -1;
        }
        sleep(2);
    }
    close(sock_conn);
    return 0;
}

