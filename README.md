# CaseStudy1

# Introduction

We have define the problem of rule based approach in the form of client and server architecture. NetMQ package is used for client and server architecture. The client and serve communicate not only in one to one fashion but several client can be open at time to communicate with the server.

# How to Run the Project

There are two folders one is AlertSystemClient.cs and other one is AlertSystemServer.cs. To run the project just run the AutoBuild.bat which is nothing but the batch file present in the AlertSystemClient.cs. We cannot start project through server as first client is going to communicate the server as first client makes the request to server and than only server will respond to client.
