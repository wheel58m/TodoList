// Austin Wheeler
// EXTRA CREDIT: Todo List
// Monday, May 1, 2023

// NOTES ----------------------------------------------------------------------/
/*
 * TODO: Create AddTodo Method.
 * TODO: Create RemoveTodo Method.
 * TODO: Create EditTodo Method.
 * TODO: Create a DisplayTodoList Method.
*/

// MAIN -----------------------------------------------------------------------/
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
            // AddTodo();
            break;
        // Remove Todo
        case '2':
            // RemoveTodo();
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
    // Read Todo List from File
    string[] todoList = File.ReadAllLines("todo-list.txt");

    // Check if Todo List is Empty
    if (todoList.Length == 0) {
        Console.WriteLine("Your Todo List is Empty!");
    } else {
        // Display Todo List
        for (int i = 0; i < todoList.Length; i++) {
            Console.WriteLine($"{i + 1}. {todoList[i]}");
        }
    }
    Console.WriteLine();
}