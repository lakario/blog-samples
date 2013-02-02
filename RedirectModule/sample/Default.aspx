<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Redirect Module Demos</title>
</head>
<style type="text/css">
    .rule
    {
        margin-bottom: 15px;
        border: 1px solid black;
        padding: 5px;
    }
</style>
<body>
    <h1>
        Redirect Module Examples</h1>
    <div class="rule">
        <strong>Rule:</strong> <tt>&lt;targetUrl="^http://website.com" destinationUrl="http://www.website.com"
            ignoreCase="true" /&gt;</tt><br />
        <strong>Description:</strong> Absolute URL redirect (permanent is implied). Does
        ignore case. Does change requested URL. Does issue HTTP 301 status code.<br />
        <strong>Example:</strong> http://website.com/foo/bar 
        =&gt; http://www.website.com/foo/bar
    </div>
    <div class="rule">
        <strong>Rule:</strong> <tt>&lt;add targetUrl="^~/FalseTarget.aspx" destinationUrl="~/RealTarget.aspx"
            ignoreCase="true" /&gt;</tt><br />
        <strong>Description:</strong> Temporary redirect. Does ignore case. Does not change
        requested URL. Does not issue HTTP 301 status code.<br />
        <br />
        <strong>Example:</strong> <a href="/FalseTarget.aspx">~/FalseTarget.aspx</a> =&gt;
        ~/RealTarget.aspx
    </div>
    <div class="rule">
        <strong>Rule:</strong> <tt>&lt;add targetUrl="^~/2ndFalseTarget.aspx" destinationUrl="~/RealTarget.aspx"
            permanent="true" /&gt;</tt><br />
        <strong>Description:</strong> Permanent redirect. Does not ignore case. Does change
        requested URL. Does issue HTTP 301 status code.<br />
        <br />
        <strong>Example:</strong> <a href="/2ndFalseTarget.aspx">~/2ndFalseTarget.aspx</a>
        =&gt; ~/RealTarget.aspx
    </div>
    <div class="rule">
        <strong>Rule:</strong> <tt>&lt;add targetUrl="^~/(Author|Category|Tool)\/([A-Za-z0\d]{8}-?[A-Za-z\d]{4}-?[A-Za-z\d]{4}-?[A-Za-z\d]{4}-?[A-Za-z\d]{12}).aspx$"
            destinationUrl="~/Pages/$1.aspx?ID=$2" /&gt;</tt><br />
        <strong>Description:</strong> Temporary redirect. Does not ignore case. Does not
        change requested URL. Does not issue HTTP 301 status code.<br />
        <br />
        <strong>Example:</strong> <a href="/Author/abc123e6-a26f-asf2-a521-ak185kfqa1fa.aspx">
            ~/Author/abc123e6-a26f-asf2-a521-ak185kfqa1fa.aspx</a> =&gt; ~/Pages/Author.aspx?ID=abc123e6-a26f-asf2-a521-ak185kfqa1fa<br />
        <strong>Example:</strong> <a href="/Category/145i1ks9-1ja2-asg1-ga52-a1b2c3d4e5f6.aspx">
            ~/Category/145i1ks9-1ja2-asg1-ga52-a1b2c3d4e5f6.aspx</a> =&gt; ~/Pages/Category.aspx?ID=145i1ks9-1ja2-asg1-ga52-a1b2c3d4e5f6<br />
        <strong>Example:</strong> <a href="/Tool/522a1kj1-14af-13kz-efgh-asr138jz1412.aspx">
            ~/Tool/522a1kj1-14af-13kz-efgh-asr138jz1412.aspx</a> =&gt; ~/Pages/Tool.aspx?ID=522a1kj1-14af-13kz-efgh-asr138jz1412
    </div>
    <div class="rule">
        <strong>Rule:</strong> <tt>&lt;add targetUrl="^~/SomeDir/(.*).aspx\??(.*)" destinationUrl="~/Pages/$1/Default.aspx?$2"
            ignoreCase="true" permanent="true" /&gt;</tt><br />
        <strong>Description:</strong> Permanent redirect. Does ignore case. Does change
        requested URL. Does issue HTTP 301 status code.<br />
        <br />
        <strong>Example:</strong> <a href="/SomeDir/Foo.aspx?bar=foo">~/SomeDir/Foo.aspx?bar=foo</a>
        =&gt; ~/Pages/Foo/Default.aspx?bar=foo<br />
        <strong>Example:</strong> <a href="/SomeDir/Bar.aspx?foo=bar">~/SomeDir/Bar.aspx?foo=bar</a>
        =&gt; ~/Pages/Bar/Default.aspx?foo=bar
    </div>
</body>
</html>
