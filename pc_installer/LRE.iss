; see the following:
;   http://www.dreamincode.net/forums/topic/180309-installing-xna-games-with-inno-setup/
;   http://pastebin.com/pAvJRqpa

#define ReleaseDir "..\Solution\MyQuestGame\MyQuestGame\bin\x86\Release"

[Setup]
AppId={{A2BF074B-F340-4715-9F09-828E7759A50D}
AppName=Lacuna Reaver
AppVersion=0.8
AppCopyright=Copyright (C) 2012 University Enterprises Corporation at CSUSB
AppPublisher=CSUSB
AppPublisherURL=http://cse.csusb.edu/
DefaultDirName={pf}\LacunaReaver
DefaultGroupName=Lacuna Reaver
;OutputDir=   ... the default is "Output" in folder with this script

[Tasks]
Name: "desktopicon"; Description: "Create a desktop icon"

[Files]
Source: {#ReleaseDir}\LacunaReaver.exe; DestDir: {app}; Flags: ignoreversion
Source: {#ReleaseDir}\MyQuest.dll; DestDir: {app}; Flags: ignoreversion
Source: {#ReleaseDir}\LacunaTextures\*; DestDir: {app}\LacunaTextures; Flags: ignoreversion recursesubdirs
Source: {#ReleaseDir}\LacunaMaps\*; DestDir: {app}\LacunaMaps; Flags: ignoreversion recursesubdirs
Source: {#ReleaseDir}\LacunaFonts\*; DestDir: {app}\LacunaFonts\; Flags: ignoreversion recursesubdirs
Source: {#ReleaseDir}\LacunaCharacters\*; DestDir: {app}\LacunaCharacters; Flags: ignoreversion recursesubdirs
Source: {#ReleaseDir}\LacunaSounds\SoundSoundBank.xsb; DestDir: {app}\LacunaSounds; Flags: ignoreversion 
Source: {#ReleaseDir}\LacunaSounds\SoundWaveBank.xwb; DestDir: {app}\LacunaSounds; Flags: ignoreversion 
Source: {#ReleaseDir}\LacunaSounds\LacunaSounds.xgs; DestDir: {app}\LacunaSounds; Flags: ignoreversion 
Source: {#ReleaseDir}\LacunaMusic\MusicSoundBank.xsb; DestDir: {app}\LacunaMusic; Flags: ignoreversion 
Source: {#ReleaseDir}\LacunaMusic\MusicWaveBank.xwb; DestDir: {app}\LacunaMusic; Flags: ignoreversion 
Source: {#ReleaseDir}\LacunaMusic\LacunaMusic.xgs; DestDir: {app}\LacunaMusic; Flags: ignoreversion 
Source: DX Redist\*; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall
Source: XNA FX Redist\xnafx40_redist.msi; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall
;Source: DotNetWebInstaller\dotNetFx40_Client_setup.exe; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall

[Icons]
Name: "{group}\Lacuna Reaver"; Filename: "{app}\LacunaReaver.exe"
Name: "{commondesktop}\Lacuna Reaver"; Filename: "{app}\LacunaReaver.exe"; Tasks: desktopicon

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"

[Run]
;Filename: {tmp}\dotNetFx40_Client_setup.exe; Flags: skipifdoesntexist; Parameters: "/q /norestart"; StatusMsg: Microsoft Framework 4.0 is beïng installed. Please wait...
Filename: {tmp}\DXSETUP.exe; Parameters: /silent; BeforeInstall: PleaseWaitMessage;
Filename: msiexec.exe; Parameters: "/quiet /i ""{tmp}\xnafx40_redist.msi"""; StatusMsg: XNA 4.0 is being installed...
Filename: {app}\LacunaReaver.exe; WorkingDir: "{app}"; Description: "Run Lacuna Reaver"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\LacunaReaver.exe"
Type: files; Name: "{app}\MyQuest.dll"
Type: dirifempty; Name: "{app}"

[Code]
var
  errorCode : integer;
  
function InitializeSetup(): Boolean;
begin
  if not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client') then 
    Begin
      if MsgBox('Microsoft .NET Framework 4 Client Profile is not present on your system.'#13#10'Lacuna Reaver depends on this component.'#13#10'You can download and install this from Microsoft.'#13#10'Do you wish to go there now?', mbConfirmation, MB_YESNO) = IDYES then
        begin
          ShellExec('open', 'http://www.microsoft.com/download/en/details.aspx?id=17113', '', '', SW_SHOW, ewNoWait, errorCode);
        end;
      Result := false;
    End
  Else
    Result := true;
end;

procedure PleaseWaitMessage();
begin
  MsgBox('This installation may take several minutes, so please be patient.'#13#10#13#10'The progess bar may remain frozen for a long time during this process.', 
         mbInformation, MB_OK);
end;

(*
var
  hasDotNet :Boolean;
 
procedure VerifyDotNet();
begin
  hasDotNet := RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client');
    if hasDotNet then
      DeleteFile(ExpandConstant('{tmp}\dotNetFx40_Client_setup.exe'));
end;
*)
(*
Function CheckForFramework : boolean;
  Var regresult : cardinal;
  Begin
    RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', regresult);
    If regresult = 0 Then
      Begin
        Result := true;
      End
    Else
      Result := false;
  End;
*)

