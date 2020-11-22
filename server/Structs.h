//
// Created by dave on 11/15/20.
//

#ifndef SERVER_STRUCTS_H
#define SERVER_STRUCTS_H

#include <mysql.h>

#define NUM_CLIENT 20

typedef struct{
    char userName[20];
    int socket;
}User;

typedef struct{
    User list[NUM_CLIENT];
    int num;
}UserList;


#endif //SERVER_STRUCTS_H
