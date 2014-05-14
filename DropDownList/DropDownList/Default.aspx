<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CustomDropDownList.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Custom DropDownList</title>
    <style>
        html, body {
            background: #f7f7f7;
        }

        fieldset {
            padding: 1em;
            margin: 1em 0;
            border: 1px solid #ccc;
        }

        .ddl {
            min-width: 200px;
        }
    </style>
    <script src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
</head>
<body>
    <form id="form" runat="server">
        <h3>Custom DropDownList Example</h3>
        <fieldset>
            <label>Gender</label>
            <acme:DropDownList ID="ddl" runat="server" CssClass="test">
                <asp:ListItem Value="0" Text="Select your gender..." />
                <asp:ListItem Value="1" Text="Male" />
                <asp:ListItem Value="2" Text="Female" />
            </acme:DropDownList>
            <asp:Label ID="lbl" runat="server" CssClass="lbl" Text="You have selected option with value: 0" />
        </fieldset>
        <asp:Button ID="btn" runat="server" Text="Submit" OnClick="btn_Click" />
        <script>
            (function ($) {
                var lbl = $('.lbl');

                $('.test').on('change', function () {
                    lbl.text('You have selected option with value: ' + $(this).val());
                });
            })(jQuery);
        </script>
    </form>
</body>
</html>
