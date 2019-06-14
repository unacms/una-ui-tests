# UNA UI Test Automation

## Running/Debugging tests

- Install .Net Core & Mono
- VSCode with extensions: C#, Ionide-fsharp, Neptune

To run tests you need to set environment variable `adminexamplepassword` which is used for the 16 testing accounts. You may add it to your `.bashrc` file.

    export adminexamplepassword=somepassword

To run tests in terminal run `dotnet test`.
On a Docker host machine you may run `./runTests.sh` to run tests in Docker angainst Selenium Grid.

Hint:
This command can be used to change owner of the files in `TestResuls` folder,
to be able to see images of the failed tests to get an idea why they are failed.
In the 2nd step file extensions are changed as for some reason canopy saves them as jpg,
while in fact the images are PNG.

```bash
sudo chown -R $USER TestResults && find ./TestResults -depth -name "*.jpg" -exec sh -c 'mv "$1" "${1%.abc}.png"' _ {} \;
```