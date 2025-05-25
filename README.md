# Password Generator Using Thread Race Condition

This project is a console-based password generator implemented in C#. It uses multiple threads and a race condition to generate passwords. The program uses a user-defined "scramble" value and password length to produce a sequence of characters based on thread execution order and counting.

## Features

- Generates passwords of user-defined length (between 8 and 20 characters).
- Allows setting a scramble value (1 to 100) which controls the number of iterations in counting threads.
- Uses two threads incrementing and decrementing a shared counter in parallel to produce a complex, hard-to-predict character.
- Displays generated password and raw ASCII values.
- Allows repeated generation with the same settings.

## How It Works

The program creates two threads for each character: one increments a counter, the other decrements it. Both run a large number of iterations defined by the scramble value multiplied by 1,000,000. The final counter value is then mapped to an ASCII character in a printable range. This approach leverages race conditions and thread timing to generate complex passwords.

## Usage

1. Run the program.
2. Enter a scramble value between 1 and 100 (values above 100 are capped to 100).
3. Enter the desired password length between 8 and 20.
4. The program will generate and display a password based on these inputs.
5. Optionally generate additional passwords with the same settings.

## Notes

- Because this approach depends on thread timing and race conditions, the same scramble value and password length will produce different passwords on different runs.
- This method is experimental and primarily for learning or demonstration purposes.
- It is not recommended for cryptographic or production use due to unpredictability and lack of cryptographic security.

## Requirements

- .NET Framework or .NET Core compatible with C# console applications.
- Basic console environment to run the program.

## Example

