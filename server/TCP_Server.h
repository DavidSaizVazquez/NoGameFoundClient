//
// Created by tania on 30/10/20.
//

#ifndef SERVER_TCP_SERVER_H
#define SERVER_TCP_SERVER_H

#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <pthread.h>

#include "SQL_Conn.h"
#include "UserListUtils.h"


void *connection_handler(void *arg);
void listener(int sock_listen);
int startTCPServer(struct sockaddr_in *serv_adr, int *sock_listen, int PORT);
int removeUser(UserList* userList, int pos);
#endif //SERVER_TCP_SERVER_H

