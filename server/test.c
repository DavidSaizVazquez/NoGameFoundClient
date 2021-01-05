#include "SQL_Conn.h"
#include "UserListUtils.h"
#include <stdio.h>
#include <mysql.h>


//
// Created by david on 5/1/21.
//
MYSQL *conn = NULL;
int main()
{
    //mysql
    char* host="127.0.0.1";
    char* user="root";
    char* passw="admin";
    char* db="GameDB";

    if (initMySQLServer(&conn,host, user, passw, db) < 0) {
        printf("Error opening Mysql");
        exit(1);
    } else {
        printf("Mysql opened\n");
    }
    int game=2;
    char *games[512];
    createGame(conn,"jesus",&game);
    UserList  players={{{"a",2},{"2",3},{"4",5}},3};
    catUsers(&players,games,"*");
    startGame(conn,games,game);
    strcpy(games,"");
    finishedGames(conn,games);
    printf(games);

    return 0;
}


