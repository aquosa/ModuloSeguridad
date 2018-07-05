<!-- SOAPACTION: http://tempuri.org/AuthenticateToken -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="/root">	
		<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
			<soapenv:Header/>
			<soapenv:Body>
				<tem:AuthenticateToken>
					<tem:token>
						<xsl:value-of select="inToken"/>
					</tem:token>
				</tem:AuthenticateToken>
			</soapenv:Body>
		</soapenv:Envelope>					
</xsl:template>
</xsl:stylesheet>
