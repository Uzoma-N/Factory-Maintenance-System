# -*- coding: utf-8 -*-
"""
Created on Tue Apr 21 17:30:23 2020

@author: uzoma
"""


def efficiency1(speed, dailyProd):
    sumspeed = 0
    sumprod = 0

    if len(speed) > 30:

        for i in range(len(speed)-30, len(speed)):
            sumspeed += float(speed[i])

        for j in range(len(dailyProd)-30, len(dailyProd)):
            sumprod += float(dailyProd[j])

        avspeed = sumspeed / 30.0
        avprod = sumprod / 30.0

    else:
        for i in range(speed.len):
            sumspeed += float(speed[i])

        for j in range(len(dailyProd)):
            sumprod += float(dailyProd[j])

        avspeed = sumspeed / len(speed)
        avprod = sumprod / len(dailyProd)



    MaxHProd = avspeed / 12.0
    TotalHProd = avprod / 24.0

    return (TotalHProd/MaxHProd) * 100.0


