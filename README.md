# CS_CommandTerminal
C# command terminal

Download entire ArdInterpTest folder, open 'program.cs' in Visual Studio.

//////////
Customize SpliceBy
//////////
Line #256: if (inp[i].ToString() == ";")
Change the ";" to whatever character you want your inputs spliced by.


//////////
Command Documentation
//////////
and;bool;bool //only takes true and false now, no call to find bool value, see getdata function in-code,see ANDcompare

b;BoolName;value //creates bool with name and value. true or false, 

i;intname;value \\or// i;intname;r;rangelow;rangehigh //creates int

s;stringname;value //create string of value

pools //prints bools

pints //prints ints

pstrings //prints strings

get;key //get value of key as string

debug; //don't call, this runs created scripts through input switch like main layer

end; //quit main layer

create;name //create a new 'script' with name. Each line of this script == a user input, runs through debug

add;anycommand //adds a command line to the script currently being edited

ps;scriptname //prints indexed lines of script name

runo;scriptname //runs script of name, can be used within scripts, one script can run any script in series

runr;amount //runs the script currently being EDITED number of times, can't be used within script 

edit;scriptname //set the script you are editing to script of name

ifi;intname1;intname2;comparator //compare int1 by comparator against int2 (may be broken)

//////////
Example
//////////
create;hello
Created CommandScript instance
add;womp;womp;womp
ps;hello
1| womp~womp~womp


