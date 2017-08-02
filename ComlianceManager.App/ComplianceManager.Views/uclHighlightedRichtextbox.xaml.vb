Public Class uclHighlightedRichtextbox


    Private lastAtPosition As Integer = 0
    Private lastSpacePosition As Integer = 0




    Public Property PlainText As String
        Get
            Return GetValue(PlainTextProperty)

        End Get

        Set(ByVal value As String)
            SetValue(PlainTextProperty, value)

        End Set
    End Property

    Public Shared ReadOnly PlainTextProperty As DependencyProperty =
                           DependencyProperty.Register("PlainText",
                           GetType(String), GetType(uclHighlightedRichtextbox),
                           New PropertyMetadata(Nothing))




    Public Property AviableUsers As List(Of Model.User)
        Get
            Return GetValue(AviableUsersProperty)
        End Get

        Set(ByVal value As List(Of Model.User))
            SetValue(AviableUsersProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AviableUsersProperty As DependencyProperty =
                           DependencyProperty.Register("AviableUsers",
                           GetType(List(Of Model.User)), GetType(uclHighlightedRichtextbox),
                           New PropertyMetadata(Nothing))



    'Private Function GetRtbText() As String

    '    Dim allTextRange As New TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd)

    '    Dim allText As String = allTextRange.Text
    '    Return allText
    'End Function




    'Private Sub HighlightWordInRichTextBox(richTextBox As RichTextBox, word As [String], color As SolidColorBrush)
    '    'Current word at the pointer
    '    Dim tr As New TextRange(richTextBox.Document.ContentEnd, rtb.Document.ContentEnd)
    '    tr.Text = word
    '    tr.ApplyPropertyValue(TextElement.BackgroundProperty, color)
    'End Sub

    Private Sub rtb_TextChanged(sender As Object, e As TextChangedEventArgs) Handles rtb.TextChanged

    End Sub

    'Private Sub uclHighlightedRichtextbox_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
    '    rtb.AutoWordSelection = Boolean


    'End Sub

    'Private Sub rtb_KeyUp(sender As Object, e As KeyEventArgs) Handles rtb.KeyUp
    '    If e.Key = Key.Q And e.KeyboardDevice.Modifiers.ToString = "Alt, Control" Then

    '        Dim position As TextPointer = rtb.Selection.[End]

    '        If position Is Nothing Then
    '            Return
    '        End If
    '        Dim positionRect As Rect = position.GetCharacterRect(LogicalDirection.Forward)
    '        PopupWindow.HorizontalOffset = positionRect.X + 25
    '        PopupWindow.VerticalOffset = positionRect.Y + 25
    '        PopupWindow.IsOpen = True

    '    End If

    '    If e.Key = Key.Space Then
    '        PopupWindow.IsOpen = False
    '    End If


    'End Sub

    'Private Sub ListBox_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
    '    'rtb.CaretPosition.InsertTextInRun(DirectCast(sender, ListBox).SelectedValue.ToString)
    '    HighlightWordInRichTextBox(rtb, AviableUsers.Where(Function(u) u.ID = DirectCast(sender, ListBox).SelectedValue).FirstOrDefault.FullName, New SolidColorBrush(Colors.LightBlue))
    '    PopupWindow.IsOpen = False
    'End Sub

    'Private Sub ListBox_KeyDown(sender As Object, e As KeyEventArgs)
    '    ' rtb.CaretPosition.InsertTextInRun(DirectCast(sender, ListBox).SelectedValue.ToString)
    '    HighlightWordInRichTextBox(rtb, AviableUsers.Where(Function(u) u.ID = DirectCast(sender, ListBox).SelectedValue).FirstOrDefault.FullName, New SolidColorBrush(Colors.LightBlue))
    '    PopupWindow.IsOpen = False
    'End Sub



End Class
