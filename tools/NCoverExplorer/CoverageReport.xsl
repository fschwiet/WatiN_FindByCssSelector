<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
	<!-- Created for NCoverExplorer by Grant Drake (see http://www.kiwidude.com/blog/) -->
	<xsl:output method="html"  doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"/>
	<xsl:template match="/">
		<xsl:apply-templates select="//coverageReport" />					
	</xsl:template>
	<xsl:template match="coverageReport">
		<html>
			<head>
				<xsl:comment>Generated by NCoverExplorer (see http://www.kiwidude.com/blog/)</xsl:comment>
				<title><xsl:value-of select="./project/@name" /> (NCoverExplorer)</title>
	<style>
		body 						{ font: small verdana, arial, helvetica; color:#000000;	}
		.coverageReportTable		{ font-size: 9px; }
		.reportHeader 				{ padding: 5px 8px 5px 8px; font-weight: bold; font-size: 12px; border: 1px solid; margin: 0px;	}
		.titleText					{ font-weight: bold; font-size: 12px; white-space: nowrap; padding: 0px; margin: 1px; }
		.subtitleText 				{ font-size: 9px; font-weight: normal; padding: 0px; margin: 1px; white-space: nowrap; }
		.projectStatistics			{ font-size: 10px; border-left: #649cc0 1px solid; white-space: nowrap;	width: 40%;	}
		.heading					{ font-weight: bold; }
		.mainTableHeaderLeft 		{ border: #dcdcdc 1px solid; font-weight: bold;	padding-left: 5px; }
		.mainTableHeader 			{ border-bottom: 1px solid; border-top: 1px solid; border-right: 1px solid;	text-align: center;	}
		.mainTableGraphHeader 		{ border-bottom: 1px solid; border-top: 1px solid; border-right: 1px solid;	text-align: left; font-weight: bold; }
		.mainTableCellItem 			{ background: #ffffff; border-left: #dcdcdc 1px solid; border-right: #dcdcdc 1px solid; padding-left: 10px; padding-right: 10px; font-weight: bold; font-size: 12px; }
		.mainTableCellData 			{ background: #ffffff; border-right: #dcdcdc 1px solid;	text-align: center;	white-space: nowrap; }
		.mainTableCellPercent 		{ background: #ffffff; font-weight: bold; white-space: nowrap; text-align: right; padding-left: 10px; }
		.mainTableCellGraph 		{ background: #ffffff; border-right: #dcdcdc 1px solid; padding-right: 5px; }
		.mainTableCellBottom		{ border-bottom: #dcdcdc 1px solid;	}
		.childTableHeader 			{ border-top: 1px solid; border-bottom: 1px solid; border-left: 1px solid; border-right: 1px solid;	font-weight: bold; padding-left: 10px; }
		.childTableCellIndentedItem { background: #ffffff; border-left: #dcdcdc 1px solid; border-right: #dcdcdc 1px solid; padding-left: 20px; padding-right: 10px; font-size: 12px; }		
		.exclusionTableCellItem 	{ background: #ffffff; border-left: #dcdcdc 1px solid; border-right: #dcdcdc 1px solid; padding-left: 10px; padding-right: 10px; }
		.projectTable				{ background: #a9d9f7; border-color: #649cc0; }
		.primaryTable				{ background: #d7eefd; border-color: #a4dafc; }
		.secondaryTable 			{ background: #f9e9b7; border-color: #f6d376; }
		.secondaryChildTable 		{ background: #fff6df; border-color: #f5e1b1; }
		.exclusionTable				{ background: #fadada; border-color: #f37f7f; }
        .graphBarNotVisited			{ font-size: 2px; border:#9c9c9c 1px solid; background:#df0000; }
        .graphBarSatisfactory		{ font-size: 2px; border:#9c9c9c 1px solid;	background:#f4f24e; }
        .graphBarVisited			{ background: #00df00; font-size: 2px; border-left:#9c9c9c 1px solid; border-top:#9c9c9c 1px solid; border-bottom:#9c9c9c 1px solid; }
	</style>
	</head>
	<body>
		<table class="coverageReportTable" cellpadding="2" cellspacing="0">
			<tbody>
			
				<xsl:variable name="reportType" select="//coverageReport/@reportTitle" />
				<xsl:variable name="threshold" select="//coverageReport/project/@acceptable" />
				
				<xsl:call-template name="header" />
				
				<xsl:call-template name="projectSummary">
					<xsl:with-param name="threshold" select="$threshold" />
				</xsl:call-template>
				
				
				<xsl:if test="$reportType = 'Module Summary' or $reportType = 'Module Namespace Summary'">
					<xsl:call-template name="moduleSummary">
						<xsl:with-param name="threshold" select="$threshold" />
					</xsl:call-template>
				</xsl:if>
				
				<xsl:if test="$reportType = 'Module Namespace Summary'">
					<xsl:call-template name="moduleNamespaceSummary">
						<xsl:with-param name="threshold" select="$threshold" />
					</xsl:call-template>
				</xsl:if>
				
				<xsl:if test="$reportType = 'Namespace Summary'">
					<xsl:call-template name="namespaceSummary">
						<xsl:with-param name="threshold" select="$threshold" />
					</xsl:call-template>
				</xsl:if>
				
				<xsl:if test="count(./exclusions) != 0">
					<xsl:call-template name="exclusionsSummary" />
				</xsl:if>
				
				<xsl:call-template name="footer" />
				
			</tbody>
		</table>
	</body>
</html>
	</xsl:template>
	
	<!-- Report Header -->
	<xsl:template name="header">
				<tr>
					<td class="projectTable reportHeader" colspan="4">
						<table width="100%">
							<tbody>
								<tr>
									<td valign="top">
										<h1 class="titleText">NCoverExplorer Coverage Report - <xsl:value-of select="./project/@name" />&#160;&#160;</h1>
										<table cellpadding="1" class="subtitleText">
											<tbody>
												<tr>
													<td class="heading">Acceptable coverage:</td>
													<td><xsl:value-of select="concat(format-number(./project/@acceptable,'#0.0'), ' %')" /></td>
												</tr>
												<tr>
													<td class="heading">Report generated on:</td>
													<td><xsl:value-of select="./@date" />&#160;at&#160;<xsl:value-of select="./@time" /></td>
												</tr>
											</tbody>
										</table>
									</td>
									<td class="projectStatistics" align="right" valign="top">
										<table cellpadding="1">
											<tbody>
												<tr>
													<td rowspan="3" valign="top" nowrap="true">Project Statistics:</td>
													<td align="right">Files:</td>
													<td align="right"><xsl:value-of select="./project/@files" /></td>
													<td rowspan="3">&#160;</td>
													<td align="right">NCLOC:</td>
													<td align="right"><xsl:value-of select="./project/@nonCommentLines" /></td>
												</tr>
												<tr>
													<td align="right">Classes:</td>
													<td align="right"><xsl:value-of select="./project/@classes" /></td>
													<td align="right">Total Pts:</td>
													<td align="right"><xsl:value-of select="./project/@sequencePoints" /></td>
												</tr>
												<tr>
													<td align="right">Members:</td>
													<td align="right"><xsl:value-of select="./project/@members" /></td>
													<td align="right">Unvisited:</td>
													<td align="right"><xsl:value-of select="./project/@unvisitedPoints" /></td>
												</tr>
											</tbody>
										</table>
									</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
	</xsl:template>
	
	<!-- Project Summary -->
	<xsl:template name="projectSummary">
		<xsl:param name="threshold" />
				<tr>
					<td colspan="4">&#160;</td>
				</tr>
				<tr>
					<td class="projectTable mainTableHeaderLeft">Project</td>
					<td class="projectTable mainTableHeader">Unvisited SeqPts</td>
					<td class="projectTable mainTableGraphHeader" colspan="2">Coverage</td>
				</tr>
			<xsl:call-template name="coverageDetail">
				<xsl:with-param name="name" select="./project/@name" />
				<xsl:with-param name="unvisitedPoints" select="./project/@unvisitedPoints" />
				<xsl:with-param name="sequencePoints" select="./project/@sequencePoints" />
				<xsl:with-param name="coverage" select="./project/@coverage" />
				<xsl:with-param name="threshold" select="$threshold" />
			</xsl:call-template>
	</xsl:template>
		
	<!-- Modules Summary -->
	<xsl:template name="moduleSummary">
		<xsl:param name="threshold" />
				<tr>
					<td colspan="3">&#160;</td>
				</tr>
				<tr>
					<td class="primaryTable mainTableHeaderLeft">Modules</td>
					<td class="primaryTable mainTableHeader">Unvisited SeqPts</td>
					<td class="primaryTable mainTableGraphHeader" colspan="2">Coverage</td>
				</tr>				
		<xsl:for-each select="//coverageReport/modules/module">
			<xsl:call-template name="coverageDetail">
				<xsl:with-param name="name" select="./@name" />
				<xsl:with-param name="unvisitedPoints" select="./@unvisitedPoints" />
				<xsl:with-param name="sequencePoints" select="./@sequencePoints" />
				<xsl:with-param name="coverage" select="./@coverage" />
				<xsl:with-param name="threshold" select="$threshold" />
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
		
	<!-- Namespaces per Module Summary -->
	<xsl:template name="moduleNamespaceSummary">
		<xsl:param name="threshold" />
		<xsl:for-each select="//coverageReport/modules/module">
				<tr>
					<td colspan="3">&#160;</td>
				</tr>
				<tr>
					<td class="secondaryTable mainTableHeaderLeft">Module</td>
					<td class="secondaryTable mainTableHeader">Unvisited SeqPts</td>
					<td class="secondaryTable mainTableGraphHeader" colspan="2">Coverage</td>
				</tr>				
			<xsl:call-template name="coverageDetailSecondary">
				<xsl:with-param name="name" select="./@name" />
				<xsl:with-param name="unvisitedPoints" select="./@unvisitedPoints" />
				<xsl:with-param name="sequencePoints" select="./@sequencePoints" />
				<xsl:with-param name="coverage" select="./@coverage" />
				<xsl:with-param name="threshold" select="$threshold" />
			</xsl:call-template>
				<tr>
					<td class="secondaryChildTable childTableHeader" colspan="4">Namespaces</td>
				</tr>				
			<xsl:for-each select="./namespace">
				<xsl:call-template name="coverageIndentedDetail">
					<xsl:with-param name="name" select="./@name" />
					<xsl:with-param name="unvisitedPoints" select="./@unvisitedPoints" />
					<xsl:with-param name="sequencePoints" select="./@sequencePoints" />
					<xsl:with-param name="coverage" select="./@coverage" />
					<xsl:with-param name="threshold" select="$threshold" />
				</xsl:call-template>
			</xsl:for-each>
		</xsl:for-each>
	</xsl:template>
		
	<!-- Namespaces Summary -->
	<xsl:template name="namespaceSummary">
		<xsl:param name="threshold" />
				<tr>
					<td colspan="3">&#160;</td>
				</tr>
				<tr>
					<td class="primaryTable mainTableHeaderLeft">Namespaces</td>
					<td class="primaryTable mainTableHeader">Unvisited SeqPts</td>
					<td class="primaryTable mainTableGraphHeader" colspan="2">Coverage</td>
				</tr>				
		<xsl:for-each select="//coverageReport/namespaces/namespace">
			<xsl:call-template name="coverageDetail">
				<xsl:with-param name="name" select="./@name" />
				<xsl:with-param name="unvisitedPoints" select="./@unvisitedPoints" />
				<xsl:with-param name="sequencePoints" select="./@sequencePoints" />
				<xsl:with-param name="coverage" select="./@coverage" />
				<xsl:with-param name="threshold" select="$threshold" />
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	
	<!-- Coverage detail row in main grid displaying a name, statistics and graph bar -->
	<xsl:template name="coverageDetail">
		<xsl:param name="name" />
		<xsl:param name="unvisitedPoints" />
		<xsl:param name="sequencePoints" />
		<xsl:param name="coverage" />
		<xsl:param name="threshold" />
				<tr>
					<td class="mainTableCellBottom mainTableCellItem"><xsl:value-of select="$name" /></td>
					<td class="mainTableCellBottom mainTableCellData"><xsl:value-of select="$unvisitedPoints" /></td>
					<td class="mainTableCellBottom mainTableCellPercent"><xsl:value-of select="concat(format-number($coverage,'#0.0'), ' %')" /></td>
					<td class="mainTableCellBottom mainTableCellGraph">
						<xsl:call-template name="detailPercent">
							<xsl:with-param name="notVisited" select="$unvisitedPoints" />
							<xsl:with-param name="total" select="$sequencePoints" />
							<xsl:with-param name="threshold" select="$threshold" />
							<xsl:with-param name="scale" select="200" />
						</xsl:call-template>
					</td>
				</tr>
	</xsl:template>
	
	<!-- Coverage detail row in secondary grid header displaying a name, statistics and graph bar -->
	<xsl:template name="coverageDetailSecondary">
		<xsl:param name="name" />
		<xsl:param name="unvisitedPoints" />
		<xsl:param name="sequencePoints" />
		<xsl:param name="coverage" />
		<xsl:param name="threshold" />
				<tr>
					<td class="mainTableCellItem"><xsl:value-of select="$name" /></td>
					<td class="mainTableCellData"><xsl:value-of select="$unvisitedPoints" /></td>
					<td class="mainTableCellPercent"><xsl:value-of select="concat(format-number($coverage,'#0.0'), ' %')" /></td>
					<td class="mainTableCellGraph">
						<xsl:call-template name="detailPercent">
							<xsl:with-param name="notVisited" select="$unvisitedPoints" />
							<xsl:with-param name="total" select="$sequencePoints" />
							<xsl:with-param name="threshold" select="$threshold" />
							<xsl:with-param name="scale" select="200" />
						</xsl:call-template>
					</td>
				</tr>
	</xsl:template>
	
	<!-- Coverage detail row with indented item name and shrunk graph bar -->
	<xsl:template name="coverageIndentedDetail">
		<xsl:param name="name" />
		<xsl:param name="unvisitedPoints" />
		<xsl:param name="sequencePoints" />
		<xsl:param name="coverage" />
		<xsl:param name="threshold" />
				<tr>
					<td class="mainTableCellBottom childTableCellIndentedItem"><xsl:value-of select="$name" /></td>
					<td class="mainTableCellBottom mainTableCellData"><xsl:value-of select="$unvisitedPoints" /></td>
					<td class="mainTableCellBottom mainTableCellPercent"><xsl:value-of select="concat(format-number($coverage,'#0.0'), ' %')" /></td>
					<td class="mainTableCellBottom mainTableCellGraph">
						<xsl:call-template name="detailPercent">
							<xsl:with-param name="notVisited" select="$unvisitedPoints" />
							<xsl:with-param name="total" select="$sequencePoints" />
							<xsl:with-param name="threshold" select="$threshold" />
							<xsl:with-param name="scale" select="170" />
						</xsl:call-template>
					</td>
				</tr>
	</xsl:template>
		
	<!-- Exclusions Summary -->
	<xsl:template name="exclusionsSummary">
			<tr>
					<td colspan="4">&#160;</td>
				</tr>
				<tr>
					<td class="exclusionTable mainTableHeaderLeft">Excluded From Coverage Results</td>
					<td class="exclusionTable mainTableGraphHeader" colspan="3">All Code Within</td>
				</tr>
		<xsl:for-each select="//coverageReport/exclusions/exclusion">
				<tr>
					<td class="mainTableCellBottom exclusionTableCellItem"><xsl:value-of select="@name" /></td>
					<td class="mainTableCellBottom mainTableCellGraph" colspan="3"><xsl:value-of select="@category" /></td>
				</tr>
		</xsl:for-each>
	</xsl:template>
	
	<!-- Footer -->
	<xsl:template name="footer">
				<tr>
					<td colspan="3">&#160;</td>
				</tr>
	</xsl:template>
	
	<!-- Draw % Green/Red/Yellow Bar -->
	<xsl:template name="detailPercent">
		<xsl:param name="notVisited" />
		<xsl:param name="total" />
		<xsl:param name="threshold" />
		<xsl:param name="scale" />
		<xsl:variable name="visited" select="$total - $notVisited" />
		<xsl:variable name="coverage" select="$visited div $total * 100"/>
		<table cellpadding="0" cellspacing="0">
		<tbody>
			<tr>
				<xsl:if test="not ($visited=0)">
					<td class="graphBarVisited" height="14">
						<xsl:attribute name="width">
							<xsl:value-of select="format-number($coverage div 100 * $scale, '0')" />
						</xsl:attribute>.
					</td>
				</xsl:if>
		        <xsl:if test="not($notVisited=0)">
					<td height="14">
						<xsl:attribute name="class">
							<xsl:if test="$coverage &gt;= $threshold">graphBarSatisfactory</xsl:if>
							<xsl:if test="$coverage &lt; $threshold">graphBarNotVisited</xsl:if>
						</xsl:attribute>
						<xsl:attribute name="width">
							<xsl:value-of select="format-number($notVisited div $total * $scale, '0')" />
						</xsl:attribute>.
					</td>
				</xsl:if>
			</tr>
			</tbody>
		</table>
	</xsl:template>
</xsl:stylesheet>