cmake_minimum_required(VERSION 3.17)
project(server)

set(CMAKE_C_STANDARD 99)

set(mysql_config --cflags --libs -lm -lpthread)
add_executable(server server.c TCP_Server.c TCP_Server.h SQL_Conn.c SQL_Conn.h Structs.h UserListUtils.c UserListUtils.h)
target_link_libraries(server mysqlclient pthread)
include_directories(/usr/include/mysql/)
link_directories(/usr/lib/mysql/)

add_executable(servertests test.c SQL_Conn.c SQL_Conn.h Structs.h UserListUtils.c UserListUtils.h)
target_link_libraries(servertests mysqlclient pthread)
include_directories(/usr/include/mysql/)
link_directories(/usr/lib/mysql/)
