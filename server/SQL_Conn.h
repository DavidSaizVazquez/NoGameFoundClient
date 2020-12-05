//
// Created by tania on 30/10/20.
//

#ifndef SERVER_SQL_CONN_H
#define SERVER_SQL_CONN_H
#include <mysql.h>
#include "Structs.h"

int initMySQLServer(MYSQL **conn,char* host, char* user, char* passw, char* db);

int loginUser(MYSQL *conn, char username[20], char password[20]);
int addUser(MYSQL *conn, char username[20], char password[20], int age, char mail[20], int spam);
int getEmail(MYSQL* conn, char username[20], char mailOutput[20]);
int setSpam(MYSQL* conn, char username[20], int spam);
int getSpam(MYSQL* conn, char username[20], int * spam);
int getAge(MYSQL* conn, char username[20], int* ageOutput);
int createGame(MYSQL* conn, char username[20], int* game);
int joinGame(MYSQL* conn, char username[20], int game);
int usersFromGame(MYSQL* conn, UserList *ans, UserList *userList, int game);
int resetSQLTable(MYSQL *conn, char table[20]);

#endif //SERVER_SQL_CONN_H
