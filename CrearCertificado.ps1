$cert = New-SelfSignedCertificate `
  -Subject "CN=Motion Corp, O=Motion Corp, C=MX" `
  -Type CodeSigningCert `
  -KeyExportPolicy Exportable `
  -KeySpec Signature `
  -KeyLength 2048 `
  -HashAlgorithm SHA256 `
  -NotAfter (Get-Date).AddYears(25) `
  -CertStoreLocation "Cert:\CurrentUser\My"

$pfxPath = "C:\Repos\Calculo ductos winUi 3\CotizadorCert.pfx"
$pwd = ConvertTo-SecureString -String "ClaveSegura123" -Force -AsPlainText

Export-PfxCertificate -Cert $cert -FilePath $pfxPath -Password $pwd
