//
// Created by dave on 11/22/20.
//

#include "UserListUtils.h"

int catUsers(UserList *userList, char * str){
    for (int i = 0; i < userList->num; i++) {
        strcat(str, userList->list[i].userName);
        strcat(str, ",");
    }
    return 0;
}