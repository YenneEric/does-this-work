- To run the code right click on the RebuildDatabase, open the Power shell and type .\RebuildDatabase.ps1
	the database [string] $Server = "(localdb)\MSSQLLocalDb",
   [string] $Database = "tuesday", it should be tuesday since that the database all of the code refers too 

- All of the tables are in the PersonData(Yes this is the name from the example but we got many errors changing 
the names so we kept it ). They are all in the Tables folder inside of here 

-The procedures for all of the SQL operations that were in our project proposal are in the procedure file about the tables
	There is also the aggregates there, for the most part the name says what they do but there is also comments

- The data file is above this and is full of the data that we inputed. 

TO KNOW
the data is not complete and only has the games for 2019 and players stats for week 1 of 2019 
all the rosters are only full on 2019 

-- there is also a models files that repersent the tables in c# which was usful for the front end

-- at the bottom of the PersonData there is a bunch of c# files that are for interface and also the c# code that will take
parameters from the user interface and apply them to the procedure by connecting to the database and then getting
the result columns

-final is the view which is WPF and it how the user can see and interact with all of the data 
there are mutiple user controls that are hidden until there button is clicked. You are able to select data from 
the database, insert, and update using the buttons and using the correct paramaters