//
// Created by tania on 30/10/20.
//

#ifndef SERVER_TCP_SERVER_H
#define SERVER_TCP_SERVER_H
#define NUM_CLIENT 20
void *connection_handler(void *arg);
void listener(int sock_listen);
int startTCPServer(struct sockaddr_in *serv_adr, int *sock_listen, int PORT);
#endif //SERVER_TCP_SERVER_H

