
# Seckim NFT Marketplace

## Add NPM to the blazor app
Create a new folder called `NpmJS`.

Open a terminal and navigate to the `NpmJS` folder and initialize npm.
```bash
npm init -y
```
Install webpack, a Javascript bundler.
```bash
npm install webpack webpack-cli --save-dev
```
Add a new folder called `src` and create a new Javascript file named `index.js` in the `src` folder.

Setup NPM in the Blazor application by adding a bpm build script that will use webpack to bundle.
```json
"scripts": {
  "build": "webpack --mode=development ./src/index.js --output-path ../wwwroot/js --output-filename index.bundle.js"
},
```

> **Note:** Set --mode=production for production builds.

To enable the npm build script, add the following pre-build events to the `csproj` file.
```xml
  <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
    <Exec Command="npm install" WorkingDirectory="NpmJS" />
    <Exec Command="npm run build" WorkingDirectory="NpmJS" />
  </Target>
```


## Dependencies and licenses
1. [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
2. [ethers](https://docs.ethers.org/v6/) - [MIT](https://docs.ethers.org/v6/license/) 