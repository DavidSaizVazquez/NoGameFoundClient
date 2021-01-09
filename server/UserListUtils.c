//
// Created by dave on 11/22/20.
//

#include "UserListUtils.h"
/**
 * concatenates all the users in a comma separated list
 * @param userList list of users
 * @param str string where the concatenated values are set
 * @return -1 if any error happens, 0 if there are no errors
 */
int catUsers(UserList *userList, char * str,char* separator){
    for (int i = 0; i < userList->num; i++) {
        strcat(str, (char*) userList->list[i].userName);
        strcat(str, separator);
    }
    return 0;
}
/**
 * finds a user in a list
 * @param userList list of users
 * @param username username to find
 * @return -1 if the user is not found and 0 if it's there
 */
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
