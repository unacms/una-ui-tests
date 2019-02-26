# UNA UI Test Automation

This command can be used to change owner of the files in `TestResuls` folder,
to be able to see images of the failed tests to get an idea why they are failed.
In the 2nd step file extensions are changed as for some reason canopy saves them as jpg,
while in fact the images are PNG.

```bash
sudo chown -R $USER TestResults && find ./TestResults -depth -name "*.jpg" -exec sh -c 'mv "$1" "${1%.abc}.png"' _ {} \;
```