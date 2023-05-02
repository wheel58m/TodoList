// Austin Wheeler
// EXTRA CREDIT: Todo List
// Monday, May 1, 2023

// NOTES ----------------------------------------------------------------------/
/*
 * TODO: Create RemoveTodo Method.
 * TODO: Create EditTodo Method.
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
    DisplayTodoList();
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
            // EditTodo();
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
void DisplayTodoList() {
    // Check if Todo List is Empty
    if (todoList.Count == 0) {
        Console.WriteLine("Your Todo List is Empty!");
    } else {
        // Display Todo List
        for (int i = 0; i < todoList.Count; i++) {
            Console.WriteLine($"{i + 1}. {todoList[i]}");
        }
    }
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

    // Add Todo to Memory
    todoList.Add(todo);

    // Add Todo to File
    File.AppendAllText("todo-list.txt", $"Incomplete: {todo}\n");
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
        Console.ForegroundColor = ConsoleColor.DarkGray;
        DisplayTodoList();
        Console.ResetColor();

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