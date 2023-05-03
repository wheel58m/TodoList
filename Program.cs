// Austin Wheeler
// EXTRA CREDIT: Todo List
// Monday, May 1, 2023

// NOTES ----------------------------------------------------------------------/
/*
 *
*/

// MAIN -----------------------------------------------------------------------/
// Read Todo List to Memory ---------------------------------------------------/
List<string> todoList = new List<string>(File.ReadAllLines("todo-list.txt"));

// Display Todo List & Menu ---------------------------------------------------/
bool error = false;
string message = null;

while (true) {
    Console.Clear();

    // Display Todo List
    Console.WriteLine("Here is your Todo List:");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    DisplayTodoList(-1);
    Console.ResetColor();

    // Display Any Messages
    if (error) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        error = false;
        message = null;
    } else {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        message = null;
    }

    // Display Instructions
    Console.WriteLine("What would you like to do? (1) Add Todo, (2) Remove Todo, (3) Edit Todo, (4) Exit");
    Console.Write("Enter a Number: ");
    ConsoleKeyInfo key = Console.ReadKey();

    switch (key.KeyChar) {
        // Add Todo
        case '1':
            AddTodo();
            break;
        // Remove Todo
        case '2':
            RemoveTodo();
            break;
        // Edit Todo
        case '3':
            EditTodo();
            break;
        // Exit
        case '4':
            Console.Clear();
            return;
        default:
            error = true;
            message = "Invalid Input. Please Try Again.";
            break;
    }
}

// METHOD: Display Todo List --------------------------------------------------/
void DisplayTodoList(int position) {
    Console.ForegroundColor = ConsoleColor.DarkGray;
    // Display Entire List if Position is Not Specified
    if (position == -1) {
        // Check if Todo List is Empty
        if (todoList.Count == 0) {
            Console.WriteLine("Your Todo List is Empty!");
        } else {
            // Display Todo List
            for (int i = 0; i < todoList.Count; i++) {
                Console.WriteLine($"{i + 1}. {todoList[i]}");
            }
        }
    } else {
        // Display Todo at Specified Position
        Console.WriteLine($"{position + 1}. {todoList[position]}");
    }
    Console.ResetColor();
    Console.WriteLine();
}

// METHOD: Add Todo -----------------------------------------------------------/
void AddTodo() {
    Console.Clear();
    Console.WriteLine("What would you like to add to your Todo List?");
    Console.Write("Enter a Todo: ");
    string todo = Console.ReadLine();

    // Check if Todo is Empty
    if (todo == "") {
        error = true;
        message = "Todo Cannot Be Empty. Please Try Again.";
        return;
    }

    // Add Status to Todo
    todo = $"Incomplete: {todo}";

    // Add Todo to Memory
    todoList.Add(todo);

    // Add Todo to File
    File.AppendAllText("todo-list.txt", $"{todo}\n");
    message = "Todo Added Successfully!";
    return;
}

// METHOD: Remove Todo --------------------------------------------------------/
void RemoveTodo() {
    // Display Todo List & Ask For Selection ----------------------------------/
    bool removalError = false;
    string removalMessage = null;

    while (true) {
        Console.Clear();

        // Display Todo List
        Console.WriteLine("Please Select a Todo to Remove:");
        DisplayTodoList(-1);

        // Display Any Error Messages
        if (removalError) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(removalMessage);
            Console.ResetColor();
            removalError = false;
            removalMessage = null;
        }

        // Ask For Selection
        Console.Write("Enter a Number: ");
        string selectionString = Console.ReadLine();

        // Check For Valid Selection
        if (selectionString != "") {
            if (int.TryParse(selectionString, out int selection)) {
                if (selection > 0 && selection <= todoList.Count) {
                    // Confirm Selection
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to remove the following Todo?");

                    // Display Todo
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{selection}. {todoList[selection - 1]}");
                    Console.ResetColor();

                    // Ask For Confirmation
                    Console.WriteLine();
                    Console.Write("Enter  Y/N: ");
                    ConsoleKeyInfo key = Console.ReadKey();

                    // Remove Todo
                    if (key.KeyChar == 'y' || key.KeyChar == 'Y') {
                        // Remove Todo From Memory
                        todoList = todoList.Where((source, index) => index != selection - 1).ToList();
                        // Remove Todo From File
                        File.WriteAllLines("todo-list.txt", todoList);
                        message = "Todo Removed Successfully!";
                        return;
                    } else if (key.KeyChar == 'n' || key.KeyChar == 'N') {
                        removalError = true;
                        removalMessage = "Todo Not Removed.";
                        return;
                    } else {
                        removalError = true;
                        removalMessage = "Invalid Input. Please Try Again.";
                        return;
                    }
                } else {
                    removalError = true;
                    removalMessage = "Selection Must Be an Existing Todo. Please Try Again.";
                }
            } else {
                removalError = true;
                removalMessage = "Input Must Be a Number. Please Try Again.";
            }
        } else {
            removalError = true;
            removalMessage = "Selection Cannot Be Empty. Please Try Again.";
        }
    }
}

// METHOD: Edit Todo ----------------------------------------------------------/
void EditTodo() {
    // Display Todo List & Ask For Selection ----------------------------------/
    bool selectionError = false;
    string selectionMessage = null;

    while (true) {
        Console.Clear();

        // Display Todo List
        Console.WriteLine("Please Select a Todo to Edit:");
        DisplayTodoList(-1);

        // Display Any Error Messages
        if (selectionError) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(selectionMessage);
            Console.ResetColor();
            selectionError = false;
            selectionMessage = null;
        }
        // Ask For Selection
        Console.Write("Enter a Number: ");
        string selectionString = Console.ReadLine();

        // Check For Valid Selection
        if (selectionString != "") {
            if (int.TryParse(selectionString, out int selection)) {
                if (selection > 0 && selection <= todoList.Count) {
                    // Display Todo & Edit Options
                    bool editError = false;
                    string editMessage = null;

                    while (true) {
                        Console.Clear();

                        // Display Todo
                        DisplayTodoList(selection - 1);

                        // Display Any Messages
                        if (editError) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(editMessage);
                            Console.ResetColor();
                            error = false;
                            message = null;
                        }

                        // Display Options
                        Console.WriteLine();
                        Console.WriteLine("What would you like to do? (1) Mark as Complete, (2) Mark as Incomplete, (3) Edit Todo, (4) Exit");
                        Console.Write("Enter a Number: ");
                        ConsoleKeyInfo key = Console.ReadKey();
                        
                        switch (key.KeyChar) {
                            // Mark Complete
                            case '1':
                                markComplete(todoList[selection - 1], selection - 1);
                                return;
                            // Mark Incomplete
                            case '2':
                                markIncomplete(todoList[selection - 1] , selection - 1);
                                break;
                            // Edit Todo
                            case '3':
                                edit(todoList[selection - 1], selection - 1);
                                break;
                            // Exit
                            case '4':
                                Console.Clear();
                                return;
                            default:
                                editError = true;
                                editMessage = "Invalid Input. Please Try Again.";
                                break;
                        }
                    }
                } else {
                    selectionError = true;
                    selectionMessage = "Selection Must Be an Existing Todo. Please Try Again.";
                }
            } else {
                selectionError = true;
                selectionMessage = "Input Must Be a Number. Please Try Again.";
            }
        } else {
            selectionError = true;
            selectionMessage = "Selection Cannot Be Empty. Please Try Again.";
        }
    }   
}

// METHOD: Mark Complete ------------------------------------------------------/
void markComplete(string todo, int position) {
    // Mark Complete in Memory
    todo = todo.Replace("Incomplete", "Complete");
    todoList[position] = todo;

    // Mark Complete in File
    File.WriteAllLines("todo-list.txt", todoList);
    message = "Todo Marked Complete Successfully!";
    return;
}

// METHOD: Mark Incomplete ----------------------------------------------------/
void markIncomplete(string todo, int position) {
    // Mark Incomplete in Memory
    todo = todo.Replace("Complete", "Incomplete");
    todoList[position] = todo;

    // Mark Incomplete in File
    File.WriteAllLines("todo-list.txt", todoList);
    message = "Todo Marked Incomplete Successfully!";
    return;
}

// METHOD: Edit ---------------------------------------------------------------/
void edit(string todo, int position) {
    // Seperate Status & Todo -------------------------------------------------/
    string[] todoArray = todo.Split(": ");
    string status = todoArray[0];
    todo = todoArray[1];

    // Display Todo & Ask For New Todo ----------------------------------------/
    bool editError = false;
    string editMessage = null;

    while (true) {
        Console.Clear();

        // Display Todo
        Console.Write($"Current Todo: ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{todo}");
        Console.ResetColor();

        // Display Any Error Messages
        if (editError) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(editMessage);
            Console.ResetColor();
            editError = false;
            editMessage = null;
        }

        // Ask For New Todo
        Console.WriteLine();
        Console.WriteLine("What would you like to change your Todo to?");
        Console.Write("Enter a Todo: ");
        string newTodo = Console.ReadLine();

        // Check if Todo is Empty
        if (newTodo == "") {
            editError = true;
            editMessage = "Todo Cannot Be Empty. Please Try Again.";
        } else {
            // Add Status to Todo
            newTodo = $"{status}: {newTodo}";
            // Edit Todo in Memory
            todoList[position] = newTodo;
            // Edit Todo in File
            File.WriteAllLines("todo-list.txt", todoList);
            return;
        }
    }
}