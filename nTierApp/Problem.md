
# Sorunun Tespiti ve zm

## Sorun

`http://localhost:5242/Account/AccessDenied?ReturnUrl=%2FAuthor` adresine gidildiinde, aadaki hata alnmaktadr:

```
InvalidOperationException: The following sections have been defined but have not been rendered by the page at '/Views/Shared/_Layout.cshtml': 'Styles'. To ignore an unrendered section call IgnoreSection("sectionName").
```

Bu hata, yetkisiz bir kullancnn erimeye alt bir sayfa iin `AccessDenied` sayfasnn gsterilmeye allrken olumaktadr.

## Sorunun Kayna

Hatann temel nedeni, `nTierApp/WebApp/Views/Account/AccessDenied.cshtml` dosyasnn iinde bir `@section Styles { ... }` blou tanmlanm olmasdr. Bu blm, sayfaya zel CSS stilleri ierir.

Ancak, bu sayfann kulland ana layout dosyas olan `nTierApp/WebApp/Views/Shared/_Layout.cshtml` ierisinde bu `Styles` blmn ileyecek (render edecek) bir komut bulunmamaktadr.

`_Layout.cshtml` dosyasnda `@RenderBody()` komutu sayfann ana ieriini ykler, ancak `@section` ile tanmlanan blmlerin ayrca belirtilmesi gerekir. Dosyada `@RenderSectionAsync("Scripts", required: false)` komutu olmasna ramen, `Styles` blm iin benzer bir komut yoktur.

## zm

Sorunu zmek iin `nTierApp/WebApp/Views/Shared/_Layout.cshtml` dosyasnn `<head>` blmne aadaki kodun eklenmesi gerekmektedir:

```csharp
@await RenderSectionAsync("Styles", required: false)
```

Bu kod, `Styles` adnda bir blm tanmlanmsa, bu blmn ieriini sayfann `<head>` etiketleri arasna yerletirir. `required: false` parametresi, bu blmn her sayfada tanmlanmasnn zorunlu olmadn belirtir. Bylece `AccessDenied.cshtml` gibi `Styles` blm ieren sayfalar dzgn alrken, bu blm iermeyen dier sayfalar etkilenmez.

### Dzenlenmesi Gereken Dosya:

`nTierApp/WebApp/Views/Shared/_Layout.cshtml`

### Eklenecek Kod:

`<head>` etiketleri ierisine, dier `<link>` etiketlerinin altna eklenebilir:

```html
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/lib/font-awesome/css/all.css" asp-append-version="true" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    @await RenderSectionAsync("Styles", required: false)
</head>
```
