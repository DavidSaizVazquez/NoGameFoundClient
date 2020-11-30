//
// Created by dave on 11/22/20.
//

#include "UserListUtils.h"

int catUsers(UserList *userList, char * str){
    for (int i = 0; i < userList->num; i++) {
        strcat(str, (char*) userList->list[i].userName);
        strcat(str, ",");
    }
    return 0;
}
int findUser(UserList *userList, char username[20]){
    int i=0;
    u_int8_t found =0;
    char answer[520]={};
    while(i<userList->num && found==0){
        if(strcmp((const char *) userList->list[i].userName, username) == 0){
            found=1;
        }
        i++;
    }
    if(found==0){
        return -1;
    }
    return 0;
}