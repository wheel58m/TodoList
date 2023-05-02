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
Console.Clear();
Console.WriteLine("Here is your Todo List:");
DisplayTodoList();

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
}