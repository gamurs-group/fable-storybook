# Fable.Storybook

Fable bindings for the [@storybook/react](https://www.npmjs.com/package/@storybook/react) NPM package.

### Nuget Packages

Stable | Prerelease
--- | ---
[![NuGet Badge](https://buildstats.info/nuget/Fable.Storybook)](https://www.nuget.org/packages/Fable.Storybook/) | [![NuGet Badge](https://buildstats.info/nuget/Fable.Storybook?includePreReleases=true)](https://www.nuget.org/packages/Fable.Storybook/)


## Getting Started

1. Configure your Fable/NPM project according to the [Storybook for React](https://storybook.js.org/basics/guide-react/)
   quick start guide.

2. Create a custom webpack configuration for Storybook, in `.storybook/webpack.config.js`:

   ```
   module.exports = {
       module: {
           rules: [
               {
                   test: /\.fs(x|proj)?$/,
                   use: {
                       loader: "fable-loader",
                       options: {
                           babel: yourCustomBabelConfig
                       }
                   }
               },

               # Any other rules your project requires (e.g. babel-loader, style-loader, file-loader)
               ...
           ]
       }
   };

   ```

3. You can now require `.fs` files directly in your `.storybook/config.js`, for example:

    ```
    import { configure } from '@storybook/react';

    // If using SASS
    import '../path/to/styles/main.sass';

    function loadStories() {
        const req = require.context('../src/', true, /\.stories\.(fs|js)$/);
        req.keys().forEach(filename => req(filename));
    }

    configure(loadStories, module);


    ```

## Example Stories

### Basic

```fsharp
/// Example stories
module Example.MyComponent

open Fable.Storybook

storiesOf("Folder/MyComponent")
    .add("First Story", fun _ -> MyComponent.render [])
    .add("Second Story", fun _ -> MyComponent.render [])
    .add("Bedtime Story", fun _ -> MyComponent.render [])
    .add("True Story", fun _ -> MyComponent.render [])
    |> ignore
```

### With Decorator - Centered

```fsharp
/// Example stories with centered decorator
module Example.MyComponent

open Fable.Storybook

storiesOf("Folder/MyComponent")
    .addDecorator(centered)
    .add("First Story", fun _ -> MyComponent.render [])
    .add("Second Story", fun _ -> MyComponent.render [])
    .add("Bedtime Story", fun _ -> MyComponent.render [])
    .add("True Story", fun _ -> MyComponent.render [])
    |> ignore
```

## Development

### Building

Make sure the following **requirements** are installed in your system:

* [dotnet SDK](https://www.microsoft.com/net/download/core) 2.0 or higher
* [node.js](https://nodejs.org) 6.11 or higher
* [yarn](https://yarnpkg.com)
* [Mono](http://www.mono-project.com/) if you're on Linux or macOS.

Then you just need to type `./build.cmd` or `./build.sh`

### Release

In order to push the package to [nuget.org](https://nuget.org) you need to add your API keys to `NUGET_KEY` environmental variable.
You can create a key [here](https://www.nuget.org/account/ApiKeys).

- Update RELEASE_NOTES with a new version, data and release notes [ReleaseNotesHelper](http://fake.build/apidocs/fake-releasenoteshelper.html).
Ex:

```
#### 0.2.0 - 30.04.2017
* FEATURE: Does cool stuff!
* BUGFIX: Fixes that silly oversight
```

- You can then use the Release target. This will:
  - make a commit bumping the version: Bump version to 0.2.0
  - publish the package to nuget
  - push a git tag

`./build.sh Release`


## How were these bindings generated

These bindings were mostly auto-generated using `ts2fable`, with a few manual tweaks:

```bash
# Install ts2fable as a global tool
yarn global add ts2fable

yarn add --dev @types/storybook__react

ts2fable node_modules/\@types/storybook__react/index.d.ts Fable.Storybook.fs
```
