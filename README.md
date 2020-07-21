# ![Creator Components logo](http://shared.lundbeckconsulting.com/image/creator-components-logo-sm.png) Creator.Components

Custom .NET Core elements by Creator framework.

Creator framework is a light weight open source framework for responsive web design and can be used on any site. The framework is created with SASS/CSS and JavaScript with main focus on styling and style elements and is managed by [Lundbeck Consulting](https://lundbeckconsulting.no/?lang=en-US).

Read more at [https://getcreatorframework.com](https://getcreatorframework.com/)

This library contains usefull functionality for [.NET ASP.NET Core](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core) web applications or sites that use JavaScript.

## GitHub

* [Distribution version](https://github.com/lundbeckconsulting/Creator-Distro)
* [Code library](https://github.com/lundbeckconsulting/Creator)
* [Creator.Components library](https://github.com/lundbeckconsulting/Creator.Components)

## Tag Helpers

The library contains the _tag helpers_:

* Dialog
* Icon
* ImageOver

## Dialog

Creates a dialog tag implementet with Creator styling. Supports all Dialog features.

The attributes _size_ and _title_ must be set.

### Size attribute

The _size_ attribute support values:

* XS
* SM
* MD
* LG
* XL

### Usage

```html
<dialog size="MD" title="My Dialog">
    <content>
        Here's my content for the dialog.
    </content>
    <footer ok-button="false">
        <button>Click me</button>
    </footer>
</dialog>
```

## Icon

> Display icons from four different set's of icons:

* FontAwesome: [https://fontawesome.com](https://fontawesome.com/icons?d=gallery)
* Friconix: [https://friconix.com](https://friconix.com/)
* CaptainIcon: [https://mariodelvalle.github.io/CaptainIconWeb](https://mariodelvalle.github.io/CaptainIconWeb/)
* DevIcons: [https://konpa.github.io/devicon](https://konpa.github.io/devicon/)

### Example

```html
<icon class="email" symbol="EnvelopeFull" title="Email" format="Anchor" href="mailto:lc@lundbeckconsulting.no" />
```

### Symbol attributes

The tag supports four icon sets and you define symbol through attributes:

* symbol: _FontAwesome_
* fi-symbol: _Friconix_
* ci-symbol: _CaptainIcons_
* di-symbol: _DevIcons_

_At lest one of the attributes must be set._

### Format attribute

You choose the output format in the _format_ attribute which supports:

* Span
* Anchor
* Button
* Div
* Italic
* Strong

### Anchor format

When format set to Anchor you access href and target through attributes with the same names:

* href
* target

### Form attributes

When output format is set to Button, the button tag is wrapped in a form. You can control the form through the attributes:

* form-method: standard form methods
* create-form: indicates if to wrap tag in a form. _Default is true_

### Friconix

> The Friconix icons support some specialized settings

Control the icon with attributes named:

* shape
* direction
* friconix-style
* thickness
* effect

### Size

You set the icon size through the attribute _size_

Size support values:

* XXS - _0.4rem_
* XS - _08rem_
* SM - _1.2rem_
* Normal - _1.6rem_
* MD - _2.6rem_
* LG - _3.2rem_
* XL - _4.4rem_
* XXL - _5.2rem_

### Button output

When output format is set to Button you set button type with attribute button with values:

* Button
* Reset
* Submit

### Text attribute

You can define a text which is placed to the right of the icon using attribute _text_. _This is specialy when format equals button._

### Color Profile

Creator supports eleven color profiles that helps you style the icon.

Use attribute _color_ with one of the values:

* Default
* Primary
* Success
* Danger
* Warning
* Info
* Light
* Dark
* Antan
* Notify
* Funk

## ImageOver

Creates an image tag that changes image src when the cursor is over the image.

### Source

Set the path to the file in the *src* attribute

### Image over source

The source for the image to display on mouse over is automatically set to src**-over**.jpg

> Example: menu-one.jpg => menu-one-over.jpg
