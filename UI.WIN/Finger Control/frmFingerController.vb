Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants
Imports System.Drawing.Imaging

Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen

Public Class frmFingerController
    Private Const DPFJ_PROBABILITY_ONE As Integer = &H7FFFFFFF
    Private rightIndex As Fmd
    Private rightThumb As Fmd
    Private anyFinger As Fmd
    Private count As Integer

    ''' <summary>
    ''' Initialize the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Identification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtIdentify.Text = String.Empty

        Dim oStaff As New Reference.clsStaff
        Dim ds = oStaff.GetData()

        For Each iLoop In ds
            If iLoop.J0 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J0.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J0.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J1 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J1.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J1.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J2 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J2.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J2.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J3 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J3.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J3.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J4 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J4.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J4.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J5 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J5.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J5.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J6 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J6.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J6.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J7 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J7.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J7.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J8 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J8.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J8.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If
            If iLoop.J9 IsNot Nothing Then
                arrFmds.Add(Importer.ImportFmd(iLoop.J9.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                fmds.Add(Importer.ImportFmd(iLoop.J9.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data, iLoop.KDSTAFF)
            End If

        Next

        If Not OpenReader() Then
            Me.Close()
        End If

        If Not StartCaptureAsync(AddressOf Me.OnCaptured) Then
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' Handler for when a fingerprint is captured.
    ''' </summary>
    ''' <param name="captureResult">contains info and data on the fingerprint capture</param>
    Public Sub OnCaptured(ByVal captureResult As CaptureResult)
        Try
            ' Check capture quality and throw an error if bad.
            If Not CheckCaptureResult(captureResult) Then Return

            Dim resultConversion As DataResult(Of Fmd) = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Formats.Fmd.ANSI)

            If resultConversion.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" & resultConversion.ResultCode.ToString())
            End If

            anyFinger = resultConversion.Data

            Dim thresholdScore As Integer = DPFJ_PROBABILITY_ONE * 1 / 100000
            Dim Cari As Boolean = False
            Dim KDSTAFF As Integer = 0

            For iLoop As Integer = 0 To fmds.Count - 1
                Dim listLoop As New List(Of Fmd)
                listLoop.Add(fmds.Keys.ElementAt(iLoop))

                Dim identifyResult = Comparison.Identify(anyFinger, 0, listLoop, thresholdScore, 2)

                If identifyResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                    Reset = True
                    Throw New Exception("" & identifyResult.ResultCode.ToString())
                End If

                If identifyResult.Indexes.Length > 0 Then
                    KDSTAFF = fmds.Item(fmds.Keys.ElementAt(iLoop))

                    'SendMessage(Action.SendMessage, KDSTAFF)

                    Dim oAbsensi As New StaffAbsensi.clsStaffAbsensi


                    'Dim dsCekBelumAbsenPulang = oAbsensi.GetDataByBelumSelesai(KDSTAFF, sCategoryAbsensi)

                    If fn_Save(KDSTAFF) = True Then
                        Cari = True
                        'My.Computer.Audio.Play("D:\Suara\TerimaKasih.wav", AudioPlayMode.WaitToComplete)

                        'SendMessage(Action.SendMessage,
                        '                "Absen " & vbTab & vbTab & ": " & Now.ToString("dd/MM/yyyy HH:mm:ss") & vbCrLf &
                        '                "Nama " & vbTab & vbTab & ": " & oStaff.GetData(KDSTAFF).NAME_DISPLAY & vbCrLf &
                        '                "Status " & vbTab & vbTab & ": " & "ABSEN MASUK BERHASIL")
                    Else
                        Cari = False
                        'My.Computer.Audio.Play("D:\Suara\CobaLagi.wav", AudioPlayMode.WaitToComplete)
                        'SendMessage(Action.SendMessage, "ABSEN GAGAL")
                    End If

                    Exit For

                Else
                    Cari = False
                    'My.Computer.Audio.Play("D:\Suara\CobaLagi.wav", AudioPlayMode.WaitToComplete)
                    'SendMessage(Action.SendMessage, "ABSEN GAGAL")
                End If
            Next

            If Cari = True Then
                Dim oStaff As New Reference.clsStaff

                My.Computer.Audio.Play("D:\Suara\TerimaKasih.wav", AudioPlayMode.WaitToComplete)

                SendMessage(Action.SendMessage,
                                "Absen " & vbTab & vbTab & ": " & Now.ToString("dd/MM/yyyy HH:mm:ss") & vbCrLf &
                                "Nama " & vbTab & vbTab & ": " & oStaff.GetData(KDSTAFF).NAME_DISPLAY & vbCrLf &
                                "Status " & vbTab & vbTab & ": " & "ABSEN MASUK BERHASIL")
            Else
                My.Computer.Audio.Play("D:\Suara\CobaLagi.wav", AudioPlayMode.WaitToComplete)
                SendMessage(Action.SendMessage, "ABSEN GAGAL")
            End If

            If fmds.Count < 0 Then
                SendMessage(Action.SendMessage, "Finger Print Kosong!")
            End If
        Catch ex As Exception
            SendMessage(Action.SendMessage, "Error:  " & ex.Message)
        End Try
    End Sub
    Private Function fn_Save(ByVal sKDSTAFF As Integer) As Boolean
        Try
            Dim oAbsensi As New StaffAbsensi.clsStaffAbsensi

            ' ***** HEADER *****
            Dim ds = oAbsensi.GetStructureHeader

            With ds
                .KDABSEN = 0
                .KDSTAFF = sKDSTAFF
                .CATEGORY = sCategoryAbsensi
                .DATECREATED = Now
                .JAMMASUK = Now
                .JAMKELUAR = Now
                .DESCRIPTION = ""
            End With

            fn_Save = oAbsensi.InsertData(ds)

        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    Private Sub Identification_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        CancelCaptureAndCloseReader(AddressOf Me.OnCaptured)
    End Sub
#Region "SendMessage"
    Public Enum Action
        SendMessage
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal action As Action, ByVal payload As String)
    Private Sub SendMessage(ByVal action As Action, ByVal payload As String)
        On Error Resume Next

        If Me.txtIdentify.InvokeRequired Then
            Dim d As New SendMessageCallback(AddressOf SendMessage)
            Me.Invoke(d, New Object() {action, payload})
        Else
            Select Case action
                Case Action.SendMessage
                    txtIdentify.Text = payload
                    txtIdentify.SelectionStart = txtIdentify.TextLength
                    txtIdentify.ScrollToCaret()

            End Select
        End If
    End Sub
#End Region
#Region "Reader"
    Dim arrFmds As New List(Of Fmd)
    Private fmds As Dictionary(Of Fmd, String) = New Dictionary(Of Fmd, String)

    Public Property Reset() As Boolean
        Get
            Return _reset
        End Get
        Set(ByVal value As Boolean)
            _reset = value
        End Set
    End Property
    Private _reset As Boolean
    Public Property CurrentReader() As Reader
        Get
            Return _currentReader
        End Get
        Set(ByVal value As Reader)
            _currentReader = value
            'SendMessage(Action.UpdateReaderState, value)
        End Set
    End Property
    Private _currentReader As Reader

    Public Function OpenReader() As Boolean
        _currentReader = ReaderCollection.GetReaders().FirstOrDefault()

        Reset = False
        Dim result As Constants.ResultCode = Constants.ResultCode.DP_DEVICE_FAILURE

        result = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)

        If result <> Constants.ResultCode.DP_SUCCESS Then
            MessageBox.Show("Error:  " & result.ToString())
            Reset = True
            Return False
        End If

        Return True
    End Function
    Public Function StartCaptureAsync(ByVal OnCaptured As Reader.CaptureCallback) As Boolean
        AddHandler _currentReader.On_Captured, OnCaptured

        If Not CaptureFingerAsync() Then
            Return False
        End If

        Return True
    End Function
    Public Function CaptureFingerAsync() As Boolean
        Try
            GetStatus()

            Dim captureResult = _currentReader.CaptureAsync(Formats.Fid.ANSI,
                                                   CaptureProcessing.DP_IMG_PROC_DEFAULT,
                                                    _currentReader.Capabilities.Resolutions(0))

            If captureResult <> ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" + captureResult.ToString())
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("Error:  " & ex.Message)
            Return False
        End Try
    End Function
    Public Sub GetStatus()
        Dim result = _currentReader.GetStatus()

        If (result <> ResultCode.DP_SUCCESS) Then
            If CurrentReader IsNot Nothing Then
                Reset = True
                Throw New Exception("" & result.ToString())
            End If
        End If

        If (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_BUSY) Then
            Thread.Sleep(50)
        ElseIf (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_NEED_CALIBRATION) Then
            _currentReader.Calibrate()
        ElseIf (_currentReader.Status.Status <> ReaderStatuses.DP_STATUS_READY) Then
            Throw New Exception("Reader Status - " & CurrentReader.Status.Status.ToString())
        End If
    End Sub
    Public Function CheckCaptureResult(ByVal captureResult As CaptureResult) As Boolean
        If captureResult.Data Is Nothing Then
            If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" & captureResult.ResultCode.ToString())
            End If

            If captureResult.Quality <> Constants.CaptureQuality.DP_QUALITY_CANCELED Then
                Throw New Exception("Quality - " & captureResult.Quality.ToString())
            End If
            Return False
        End If
        Return True
    End Function
    Public Function CreateBitmap(ByVal bytes As [Byte](), ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim rgbBytes As Byte() = New Byte(bytes.Length * 3 - 1) {}

        For i As Integer = 0 To bytes.Length - 1
            rgbBytes((i * 3)) = bytes(i)
            rgbBytes((i * 3) + 1) = bytes(i)
            rgbBytes((i * 3) + 2) = bytes(i)
        Next
        Dim bmp As New Bitmap(width, height, PixelFormat.Format24bppRgb)

        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[WriteOnly], PixelFormat.Format24bppRgb)

        For i As Integer = 0 To bmp.Height - 1
            Dim p As New IntPtr(data.Scan0.ToInt64() + data.Stride * i)
            System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3)
        Next

        bmp.UnlockBits(data)

        Return bmp
    End Function
    Public Sub CancelCaptureAndCloseReader(ByVal OnCaptured As Reader.CaptureCallback)
        If _currentReader IsNot Nothing Then
            ' Dispose of reader handle and unhook reader events.
            CurrentReader.Dispose()

            If (Reset) Then
                CurrentReader = Nothing
            End If
        End If
    End Sub
#End Region
End Class