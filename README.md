# Password Generator with Thread Race Condition

This is a simple console program written in C# that generates passwords using a race condition between two threads. The threads increment and decrement a shared counter many times, and the final counter value determines each character in the password.

It is just a fun project.

## Use?

1. Run the program.
2. Enter a scramble value between 1 and 100. (Higher values are capped at 100.)
3. Enter the desired password length (between 8 and 20).
4. The program will generate a password based on these inputs.
5. You can generate more passwords with the same settings if you want.

## How?

- For each character, two threads run in parallel: one counts up, the other counts down.
- The number of iterations is `scramble value * 1,000,000`.
- Because the threads run concurrently, their timing affects the final counter value.
- The final count is converted into a printable ASCII character.
- This method makes passwords unpredictable but not reproducible with the same input.

## Notes

- The password changes every time, even with the same scramble and length.
- This project is more of an experiment in using threads and race conditions than a secure password generator.
- It’s not recommended to use this in real security-sensitive applications.
