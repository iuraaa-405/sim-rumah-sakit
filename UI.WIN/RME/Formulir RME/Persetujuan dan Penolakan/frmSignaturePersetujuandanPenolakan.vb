Imports DataAccess
Imports System.Linq
Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants
Imports System.Drawing.Imaging

Public Class frmSignaturePersetujuandanPenolakan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
    Private WithEvents enrollmentControl As DPCtlUruNet.EnrollmentControl
    Private _Previous As System.Nullable(Of Point) = Nothing
    Private sSEQ As Integer = 0

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, ByVal SEQ As Integer)
        oFormMode = FormMode
        txtCODE.Text = KDKUNJUNGAN
        sSEQ = SEQ
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Signature" & " - Edit Form"
        fn_ChangeFormState()
        isLoad = True

        Try
            CurrentReader = ReaderCollection.GetReaders().FirstOrDefault()

            If enrollmentControl IsNot Nothing Then
                _enrollmentControl.Reader = CurrentReader
            Else
                enrollmentControl = New DPCtlUruNet.EnrollmentControl(CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)
                enrollmentControl.BackColor = System.Drawing.SystemColors.Window
                enrollmentControl.Location = New System.Drawing.Point(20, 210)
                enrollmentControl.Name = "ctlEnrollmentControl"
                enrollmentControl.Size = New System.Drawing.Size(482, 346)
                enrollmentControl.TabIndex = 0
            End If

            Me.Controls.Add(enrollmentControl)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
        enrollmentControl.Cancel()
    End Sub
    Private Sub fn_ChangeFormState()
        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtNAMA.Properties.ReadOnly = Status
        txtALAMAT.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        chkISACTIVE.Checked = True
        txtNAMA.ResetText()
        txtIDENTITAS.ResetText()
        txtALAMAT.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSignature.GetData(txtCODE.Text, sSEQ)

            With ds
                txtCODE.Text = .KDKUNJUNGAN
                txtIDENTITAS.Text = .IDENTITAS
                txtNAMA.Text = .NAMA
                txtALAMAT.Text = .ALAMAT
                chkISACTIVE.Checked = .ISACTIVE

                If ds.J0 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J0.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J1 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J1.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J2 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J2.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J3 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J3.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J4 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J4.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J5 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J5.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J6 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J6.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J7 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J7.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J8 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J8.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If
                If ds.J9 IsNot Nothing Then
                    Fmds.Add(0, Importer.ImportFmd(ds.J9.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                End If

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNAMA.Text = String.Empty Then
                MsgBox("Dibutuhkan Nama", MsgBoxStyle.Exclamation, Me.Text)
                txtNAMA.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtIDENTITAS.Text = String.Empty Then
                MsgBox("Dibutuhkan No Identitas", MsgBoxStyle.Exclamation, Me.Text)
                txtIDENTITAS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtALAMAT.Text = String.Empty Then
                MsgBox("Dibutuhkan Alamat", MsgBoxStyle.Exclamation, Me.Text)
                txtALAMAT.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSignature.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSignature.GetData(txtCODE.Text, sSEQ).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDKUNJUNGAN = txtCODE.Text
                .SEQ = sSEQ
                .IDENTITAS = txtIDENTITAS.Text.ToString.Trim
                .NAMA = txtNAMA.Text.Trim.ToUpper
                .ALAMAT = txtALAMAT.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked

                If Fmds.ContainsKey(0) = True Then
                    .J0 = Fmds.Item(0).Bytes
                Else
                    Try
                        .J0 = oSignature.GetData(txtCODE.Text, sSEQ).J0
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(1) = True Then
                    .J1 = Fmds.Item(1).Bytes
                Else
                    Try
                        .J1 = oSignature.GetData(txtCODE.Text, sSEQ).J1
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(2) = True Then
                    .J2 = Fmds.Item(2).Bytes
                Else
                    Try
                        .J2 = oSignature.GetData(txtCODE.Text, sSEQ).J2
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(3) = True Then
                    .J3 = Fmds.Item(3).Bytes
                Else
                    Try
                        .J3 = oSignature.GetData(txtCODE.Text, sSEQ).J3
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(4) = True Then
                    .J4 = Fmds.Item(4).Bytes
                Else
                    Try
                        .J4 = oSignature.GetData(txtCODE.Text, sSEQ).J4
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(5) = True Then
                    .J5 = Fmds.Item(5).Bytes
                Else
                    Try
                        .J5 = oSignature.GetData(txtCODE.Text, sSEQ).J5
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(6) = True Then
                    .J6 = Fmds.Item(6).Bytes
                Else
                    Try
                        .J6 = oSignature.GetData(txtCODE.Text, sSEQ).J6
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(7) = True Then
                    .J7 = Fmds.Item(7).Bytes
                Else
                    Try
                        .J7 = oSignature.GetData(txtCODE.Text, sSEQ).J7
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(8) = True Then
                    .J8 = Fmds.Item(8).Bytes
                Else
                    Try
                        .J8 = oSignature.GetData(txtCODE.Text, sSEQ).J8
                    Catch ex As Exception

                    End Try
                End If
                If Fmds.ContainsKey(9) = True Then
                    .J9 = Fmds.Item(9).Bytes
                Else
                    Try
                        .J9 = oSignature.GetData(txtCODE.Text, sSEQ).J9
                    Catch ex As Exception

                    End Try
                End If

                .DESCRIPTION = ""
                .KDUSER = sUserID

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSignature.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSignature.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadButton()
        'Dim btn() = {btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9}

        'For iLoop As Integer = 0 To btn.Count - 1
        '    If Fmds.ContainsKey(iLoop) = True Then
        '        btn(iLoop).Appearance.BackColor = Color.Green
        '    Else
        '        btn(iLoop).Appearance.BackColor = Color.Black
        '    End If
        'Next
    End Sub
#End Region
#Region "Reader"
    Public Property Fmds() As Dictionary(Of Int16, Fmd)
        Get
            Return _fmds
        End Get
        Set(ByVal value As Dictionary(Of Int16, Fmd))
            _fmds = value
        End Set
    End Property
    Private _fmds As Dictionary(Of Int16, Fmd) = New Dictionary(Of Int16, Fmd)
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
#Region "Enrollment Control Events"
    Private Sub SendMessage(ByVal message As String)
        txtMessage.Text += message & vbCr & vbLf & vbCr & vbLf
        txtMessage.SelectionStart = txtMessage.TextLength
        txtMessage.ScrollToCaret()
    End Sub
    Private Sub enrollment_OnCancel(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnCancel
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnCancel:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnCancel:  No Reader Connected, finger " & fingerPosition)
        End If
    End Sub

    Private Sub enrollment_OnCaptured(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal captureResult As CaptureResult, ByVal fingerPosition As Integer) Handles enrollmentControl.OnCaptured
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage(("OnCaptured:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition & ", quality ") + captureResult.Quality.ToString())
        Else
            SendMessage("OnCaptured:  No Reader Connected, finger " & fingerPosition)
        End If

        If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
            If CurrentReader IsNot Nothing Then
                CurrentReader.Dispose()
                CurrentReader = Nothing
            End If

            ' Disconnect reader from enrollment control
            _enrollmentControl.Reader = Nothing
            MessageBox.Show("Error:  " & captureResult.ResultCode.ToString())
        Else
            If captureResult.Data IsNot Nothing Then
                For Each fiv As Fid.Fiv In captureResult.Data.Views
                    picFinger.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height)
                Next
            End If
        End If
    End Sub

    Private Sub enrollment_OnDelete(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnDelete
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnDelete:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnDelete:  No Reader Connected, finger " & fingerPosition)
        End If

        Fmds.Remove(fingerPosition)
    End Sub
    Private Sub enrollment_OnEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As DataResult(Of Fmd), ByVal fingerPosition As Integer) Handles enrollmentControl.OnEnroll
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnEnroll:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnEnroll:  No Reader Connected, finger " & fingerPosition)
        End If

        ' Save the enrollment to file.
        If result IsNot Nothing AndAlso result.Data IsNot Nothing Then
            If Fmds.ContainsKey(fingerPosition) = True Then
                Fmds.Remove(fingerPosition)
            End If

            Fmds.Add(fingerPosition, result.Data)
        End If

        fn_LoadButton()

    End Sub
    Private Sub enrollment_OnStartEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnStartEnroll
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnStartEnroll:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnStartEnroll:  No Reader Connected, finger " & fingerPosition)
        End If

    End Sub

#End Region
End Class