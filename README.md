# LuceneDotNetDemo
This is a simple Lucene Dot Net demo project. This pretty much covers the very basics of lucene indexing. A great starting point in learning LUCENE.

REMINDER: BE SURE TO CREATE A C:\Lucene\Index FOLDER on your machine.

This allows you to search through Songs DATA (with TITLE, ARTIST, LYRICS, RELEASED DATE fields) using LUCENE.NET

It has two projects 
(1) SearchSongLucene - which is a very simple WINFORMS with very simple UI
(2) SongsSearchBL - contains simple logic that writes indexes, and search them.

HOW TO USE: 

Click the Index Data to start writing SONGS DATA (which is hard coded for now) to the LUCENE folder
C:\Lucene\Index


After that, start learning lucene searching. You can switch to different search strategies by changing a bit of code in Form1.cs, method btnSearch_Click.
I have placed a comment --> TODO: <-- so you can easily locate that

