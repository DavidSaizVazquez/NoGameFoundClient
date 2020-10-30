//
// Created by tania on 30/10/20.
//

#ifndef SERVER_SQL_CONN_H
#define SERVER_SQL_CONN_H
#include <mysql.h>

int initMySQLServer(MYSQL **conn);

int loginUser(MYSQL *conn, char username[20], char password[20]);
int addUser(MYSQL *conn, char username[20], char password[20], int age, char mail[20], int spam);
int getEmail(MYSQL* conn, char username[20], char mailOutput[20]);
int setSpam(MYSQL* conn, char username[20], int spam);
int getSpam(MYSQL* conn, char username[20], int * spam);
int getAge(MYSQL* conn, char username[20], int* ageOutput);

#endif //SERVER_SQL_CONN_H
