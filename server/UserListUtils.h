//
// Created by dave on 11/22/20.
//

#ifndef SERVER_USERLISTUTILS_H
#define SERVER_USERLISTUTILS_H
#include "Structs.h"
#include "string.h"


int catUsers(UserList *userList, char * str);
int findUser(UserList *userList, char username[20]);


#endif //SERVER_USERLISTUTILS_H
