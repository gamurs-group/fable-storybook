/// Fable bindings for @storybook/react
module rec Fable.Storybook

open Fable.Core
open Fable.Import.React

type [<AllowNullLiteral>] IExports =
    abstract addDecorator : decorator:StoryDecorator -> unit
    abstract addParameters : parameters:DecoratorParameters -> unit
    abstract clearDecorators : unit -> unit
    abstract configure : fn:(unit -> unit) * ``module``:obj -> unit
    abstract setAddon : addon:obj -> unit
    abstract storiesOf : name:string * ``module``:obj -> Story
    abstract forceReRender : unit -> unit
    abstract getStorybook : unit -> ResizeArray<StoryBucket>

type Renderable = ReactElement

type RenderFunction = unit -> Renderable

type [<AllowNullLiteral>] DecoratorParameters =
    [<Emit "$0[$1]{{=$2}}">] abstract Item : key:string -> obj option with get, set

type [<AllowNullLiteral>] StoryDecorator =
    [<Emit "$0($1...)">] abstract Invoke : story:RenderFunction * context:StoryDecoratorInvokeContext -> Renderable option

type [<AllowNullLiteral>] StoryDecoratorInvokeContext =
    abstract kind : string with get, set
    abstract story : string with get, set

type [<AllowNullLiteral>] Story =
    abstract kind : string
    abstract add : storyName:string * callback:RenderFunction * ?parameters:DecoratorParameters -> Story
    abstract addDecorator : decorator:StoryDecorator -> Story
    abstract addParameters : parameters:DecoratorParameters -> Story

type [<AllowNullLiteral>] StoryObject =
    abstract name : string with get, set
    abstract render : RenderFunction with get, set

type [<AllowNullLiteral>] StoryBucket =
    abstract kind : string with get, set
    abstract stories : ResizeArray<StoryObject> with get, set

/// Access a reference to the Webpack 'module' global variable
let [<Emit("module")>] webpackModule<'T> : 'T = jsNative
//Fable.Import.JS.Glo

/// import * from '@storybook/react';
[<Import("*", from="@storybook/react")>]
let Storybook : IExports = jsNative

/// <summary>
/// Wrapper for Storybook.storiesOf that takes care binding the webpackModule parameter.

let storiesOf name = Storybook.storiesOf (name, webpackModule)

/// import centered from '@storybook/addon-centered';
[<Import("default", from="@storybook/addon-centered")>]
let centered : StoryDecorator = jsNative
