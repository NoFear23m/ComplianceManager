Imports System.Windows
Imports System.Windows.Interactivity
Imports System.Windows.Controls

Public Class IntellisenseBehavior
        Inherits Behavior(Of RichTextBox)

        Public Sub New()
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
        End Sub
    End Class
