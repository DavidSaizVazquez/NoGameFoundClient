cmake_minimum_required(VERSION 3.17)
project(server)

set(CMAKE_C_STANDARD 99)

set(mysql_config --cflags --libs -lm)
add_executable(server server2.c TCP_Server.c TCP_Server.h SQL_Conn.c SQL_Conn.h)
target_link_libraries(server mysqlclient)
include_directories(/usr/include/mysql/)
link_directories(/usr/lib/mysql/)