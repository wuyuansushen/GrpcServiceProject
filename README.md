# GrpcServiceProject

### 1.Minimum TLS Version

Only set TLS version to `TLS 1.2`, because both TLS 1.0 and 1.1 are deprecated and TLS 1.3 will cause error with `ProtocolVersion`in gRPC.

### 2.Enable gRPC in CDN Panel.

### 3.Add `A` Record in CDN-DNS Panel

### 4.Install `aspnetcore-runtime-5.0`

### 5.Firewalld Adjustment

### 6.Install httpd and configure reverse proxy

```
<VirtualHost *:443>
 RewriteEngine  on
SSLEngine on
ServerName <SubDomain>
SSLCertificateFile <PEMCertificate>
SSLCertificateKeyFile <PEMPrivateKey>
ProxyPreserveHost On
ProxyPass "/" "h2c://127.0.0.1:6000/"		#Use "h2://" as header with HTTP/2 (TLS)
ProxyPassReverse "/" "h2c://127.0.0.1:6000/"
</VirtualHost>
```

### 7.Add systemd service

Add service file `/etc/systemd/system/grpc.service`.

```
[Unit]
Description=.NET Web App running on CentOS 8

[Service]
WorkingDirectory=/var/www/grpc
ExecStart=/usr/bin/dotnet /var/www/grpc/GrpcServiceProject.dll
KillSignal=SIGINT
SyslogIdentifier=dotnet-ALL
User=apache
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```
```
systemctl daemon-reload
```

### 8.Make Server Content directory

```
mkdir -p /var/www/grpc
```

### 9.Deploy Script

Requirement: Go to the publish directory (`./GrpcServiceProject/bin/Release/net5.0/publish`)

:warning: The following command must be in one line rather than seperated to multiple lines.

```
rm -rf ./publish.zip;zip -qr publish.zip ./*;export serIp=[YourServerIp];export sshdPort=[YourSshdPort];scp -P ${sshdPort} -r ./publish.zip root@${serIp}:~/publish.zip;ssh root@${serIp} -p ${sshdPort} "rm -rf /var/www/grpc/*;unzip -qd /var/www/grpc/ ~/publish.zip;rm -rf ~/publish.zip;systemctl restart grpc;";rm -rf ./publish.zip;
```
