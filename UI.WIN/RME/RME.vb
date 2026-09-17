Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Namespace RME
    Public Class clsRME
#Region "Function"
        Private oSetKoneksi As New Brigging.clsSetKoneksi

        Public Sub DeleteDirectory(path As String)
            Try
                If IO.Directory.Exists(path) Then
                    'Delete all files from the Directory
                    For Each filepath As String In IO.Directory.GetFiles(path)
                        IO.File.Delete(filepath)
                    Next
                    'Delete all child Directories
                    For Each dir As String In IO.Directory.GetDirectories(path)
                        DeleteDirectory(dir)
                    Next
                    'Delete a Directory
                    IO.Directory.Delete(path)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement, MsgBoxStyle.Exclamation)
            End Try
        End Sub
        Public Function IntegerToRoman(IntNumberValue As Integer) As String
            Try
                Dim RomanNumbers As New Dictionary(Of String, Integer)()
                RomanNumbers.Add("M", 1000)
                RomanNumbers.Add("CM", 900)
                RomanNumbers.Add("D", 500)
                RomanNumbers.Add("CD", 400)
                RomanNumbers.Add("C", 100)
                RomanNumbers.Add("XC", 90)
                RomanNumbers.Add("L", 50)
                RomanNumbers.Add("XL", 40)
                RomanNumbers.Add("X", 10)
                RomanNumbers.Add("IX", 9)
                RomanNumbers.Add("V", 5)
                RomanNumbers.Add("IV", 4)
                RomanNumbers.Add("I", 1)

                Dim result As String = ""

                For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
                    While IntNumberValue >= pair.Value
                        IntNumberValue -= pair.Value
                        result += pair.Key
                    End While
                Next

                IntegerToRoman = result
            Catch oErr As Exception
                IntegerToRoman = ""
                MsgBox(Statement.ErrorStatement, MsgBoxStyle.Exclamation)
            End Try
        End Function
        Public Function fn_AlamatSimpanFile() As String
            Try
                fn_AlamatSimpanFile = ""
                'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "SIMPANPDFCASMIX")
                'If dsDataSetKoneksi IsNot Nothing Then
                '    fn_AlamatSimpanFile = dsDataSetKoneksi.ALAMATWEB
                'Else
                '    fn_AlamatSimpanFile = ""
                'End If
            Catch oErr As Exception
                fn_AlamatSimpanFile = ""
                MsgBox(Statement.ErrorStatement, MsgBoxStyle.Exclamation)
            End Try
        End Function
        Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
            Try
                Dim years As Long
                Dim months As Long
                Dim days As Long
                Dim yearWord As String
                Dim monthWord As String
                Dim dayWord As String

                ' menghitung tahun
                years = DateDiff("yyyy", tgllahir, dateNow)
                If Month(tgllahir) > Month(dateNow) Then
                    years = years - 1
                ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
                    years = years - 1
                ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
                    'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
                End If
                ' menghitung bulan
                tgllahir = DateAdd("yyyy", years, tgllahir)
                months = DateDiff("m", tgllahir, dateNow)
                If tgllahir.Day > dateNow.Day Then
                    months = months - 1
                ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
                    months = months - 1
                End If
                tgllahir = DateAdd("m", months, tgllahir)
                ' menghitung hari
                days = DateDiff("d", tgllahir, dateNow)

                yearWord = IIf(years = 0, "", years & " Tahun ")
                monthWord = IIf(months = 0, "", months & " Bulan ")
                dayWord = IIf(days = 0, "", days & " Hari ")
                'calculateAge = yearWord & monthWord & dayWord
                'calculateAge = Trim(calculateAge)

                GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
            Catch oErr As Exception
                GetUmurPasien = ""
                MsgBox(Statement.ErrorStatement, MsgBoxStyle.Exclamation)
            End Try
        End Function
#End Region
#Region "Lookup"
        '#Region "Scrol Mouse"
        '    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        '        'If lblScroll.Text = "" Then Exit Sub

        '        If e.Delta > 0 Then
        '            Trace.WriteLine("Scrolled up!")
        '            fn_ScrollPage(True)
        '        Else
        '            Trace.WriteLine("Scrolled down!")
        '            fn_ScrollPage(False)
        '        End If
        '    End Sub
        '    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        '        Dim myView As Point = Me.panelCPPT.AutoScrollPosition

        '        Dim scrollchange As Integer = 50

        '        If isUp Then
        '            'up
        '            myView.X = -myView.X
        '            myView.Y = -scrollchange - myView.Y
        '        Else
        '            'down
        '            myView.X = -myView.X
        '            myView.Y = scrollchange - myView.Y
        '        End If

        '        Me.panelCPPT.AutoScrollPosition = myView
        '    End Sub
        '#End Region
#End Region
    End Class
End Namespace