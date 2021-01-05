//
// Created by dave on 11/15/20.
//

#ifndef SERVER_STRUCTS_H
#define SERVER_STRUCTS_H

#include <mysql.h>

#define NUM_CLIENT 20
//username definition
typedef char UserName[20];
// struct that contains a user
typedef struct{
    UserName userName;
    int socket;
}User;
//struct that contains a positon
typedef struct {
    float x;
    float y;
} Position;
//list of users
typedef struct{
    User list[NUM_CLIENT];
    int num;
}UserList;


#endif //SERVER_STRUCTS_H
