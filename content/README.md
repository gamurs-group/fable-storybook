# fable-storybook

Fable bindings for `@storybook/react`.

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

    import '../styles/main.sass';

    function loadStories() {
        const req = require.context('../src/', true, /\.stories\.(fs|js)$/);
        req.keys().forEach(filename => req(filename));
    }

    configure(loadStories, module);


    ```

## Example Stories

You can then wrote stories as in the following example:

```fsharp

/// Example stories
module Example.MyComponent

open Fable.Storybook.React

storiesOf("Folder/MyComponent")
    .add("First Story", fun _ -> MyComponent.render [])
    .add("Second Story", fun _ -> MyComponent.render [])
    .add("Bedtime Story", fun _ -> MyComponent.render [])
    .add("True Story", fun _ -> MyComponent.render [])
    |> ignore

```

## How were these bindings generated

These bindings were mostly auto-generated using `ts2fable`, with a few manual tweaks:

```bash
# Install ts2fable as a global tool
yarn global add ts2fablegit clone

yarn add --dev @types/storybook__react

ts2fable node_modules/\@types/storybook__react/index.d.ts StorybookReact.fs
```
