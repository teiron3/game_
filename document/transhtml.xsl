<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match="doc">
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match="assembly">
  </xsl:template>

  <xsl:template match="members">
    <html> <body>
    <h1>cs document</h1>
      <xsl:for-each select="member">
      <xsl:sort select="@name"/>
        <p>
          <font size="5" color="red"><xsl:value-of select="summary"/></font><br/>
          <font size="5" color="blue"><xsl:value-of select="@name"/></font>
        </p>
        <xsl:for-each select="param">
          <p>引数　<xsl:value-of select="@name"/>　・・・
             <xsl:value-of select="."/>
          </p>
        </xsl:for-each>
        <xsl:for-each select="returns">
        <p>戻り値・・・
          <xsl:value-of select="."/>
        </p>
        </xsl:for-each>
        <xsl:for-each select="remarks">
          <p>
            <xsl:value-of select="."/>
          </p>
        </xsl:for-each>
        <br/>
      </xsl:for-each>
    <xsl:apply-templates/>
      </body> </html>
  </xsl:template>

  <xsl:template match="summary">
  </xsl:template>
  <xsl:template match="param">
  </xsl:template>
  <xsl:template match="remarks">
  </xsl:template>
  <xsl:template match="returns">
  </xsl:template>

</xsl:stylesheet>