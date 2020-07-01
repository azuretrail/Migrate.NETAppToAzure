

# Variable setup
$storageAccName='[storageAccName]'
$password='[storageAccPassword]'
$passwordSS = ConvertTo-SecureString $Password –asplaintext –force 
$storageAccUserName= '[StorageAccUserName]'
$iisAppPoolName = "azureFS"
$iisSiteName = "Default Web Site"
$iisAppPoolDotNetVersion = "v4.0"
$appDirectoryPath = "C:\Deployments\EmployeeManagement"
$sitePath ="%SystemDrive%\inetpub\wwwroot"

#Enable Server as a Webserver to install IIS
Add-WindowsFeature Web-Server -IncludeManagementTools -IncludeAllSubFeature

$deploymentsFolder = "C:\Deployments\EmployeeManagement"
if(!(Test-Path -Path $deploymentsFolder )){
	md $deploymentsFolder
} 

#Create a mapped drive to azure storage account containing installation files
net use Z: $storageAccName /u:AZURE\$storageAccUserName $password
$sourceDirectory  = "Z:\*"
Copy-item -Force -Recurse -Verbose $sourceDirectory -Destination $deploymentsFolder

 
#
Import-Module WebAdministration

#create local machine user that will be used in IIS by the app pool to access the azure fileshare
New-LocalUser $storageAccUserName -Password $passwordSS -FullName "local azure user" -Description "local azure storage account user for IIS."
net localgroup IIS_IUSRS $storageAccUserName /add

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $iisAppPoolName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
}
Start-WebAppPool -Name $appPool

#navigate to the sites root
cd IIS:\Sites\

#set the advanced setting identity on the app pool
Set-ItemProperty IIS:\AppPools\azureFS -name processModel -value @{userName=$storageAccUserName;password=$password;identitytype=3}

#create the webapp
New-WebApplication -Name "EmployeeManagement" -Site $iisSiteName -PhysicalPath $appDirectoryPath  -ApplicationPool $iisAppPoolName 















