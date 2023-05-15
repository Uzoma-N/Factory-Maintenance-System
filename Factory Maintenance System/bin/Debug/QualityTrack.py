# -*- coding: utf-8 -*-
"""
Created on Tue Apr 21 18:33:49 2020

@author: uzoma
"""


def QualityTrack(BadProd, totalProd):
    sumbad = 0
    sumprod = 0

    if len(BadProd) > 30:

        for i in range(len(BadProd) - 30, len(BadProd)):
            sumbad += float(BadProd[i])

        for j in range(len(totalProd) - 30, len(totalProd)):
            sumprod += float(totalProd[j])

        avbad = sumbad / 30.0
        avprod = sumprod / 30.0

    else:
        for i in range(len(BadProd) ):
            sumbad += float(BadProd[i])

        for j in range(len(totalProd)):
            sumprod += float(totalProd[j])

        avbad = sumbad / len(BadProd) 
        avprod = sumprod / len(totalProd)


    Badper = (avbad / avprod) * 100.0

    return 100.0 - Badper


