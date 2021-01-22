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
int deleteUser(MYSQL *conn, char username[20]);
int getEmail(MYSQL* conn, char username[20], char mailOutput[20]);
int setSpam(MYSQL* conn, char username[20], int spam);
int getSpam(MYSQL* conn, char username[20], int * spam);
int getAge(MYSQL* conn, char username[20], int* ageOutput);

int createGame(MYSQL* conn, char username[20], int* game);
int joinGame(MYSQL* conn, char username[20], int game);
int startGame(MYSQL* conn, char* players, int game);
int setGameScore(MYSQL* conn, int score, int game);
int usersFromGame(MYSQL* conn, UserList *ans, UserList *userList, int game);
int resetSQLTable(MYSQL *conn, char table[20]);
int ongoingGames(MYSQL* conn, char games[512]);
int finishedGames(MYSQL* conn, char games[512]);
int playersPlayedWith(MYSQL* conn, char *players,char * name);
int gamesWithinDates(MYSQL* conn, char games[512],char * sDay, char * fDay);
int exitGame(MYSQL* conn, char username[20], int game);
#endif //SERVER_SQL_CONN_H
