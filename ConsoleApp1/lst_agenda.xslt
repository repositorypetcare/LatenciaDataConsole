<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="/root">
		<html>
			<body>
				<table border="on" cellspacing="1" cellpadding="5">
					<!--<thead>
						<th>Invoice Number</th>
						<th>Customer Name</th>
						<th>Total Purchase</th>
					</thead>-->
					<tbody>
						<xsl:for-each select="prof">
							<tr>
								<td>
									<xsl:value-of select="@idprof"/>
								</td>
								<td>
									<xsl:value-of select="@nome"/>
								</td>
							</tr>
							<xsl:for-each select="item">
								<tr>
									<td>
										<xsl:value-of select="@date" />
									</td>
									<td>
										<xsl:value-of select="@flag" />
									</td>
								</tr>
							</xsl:for-each>
						</xsl:for-each>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
