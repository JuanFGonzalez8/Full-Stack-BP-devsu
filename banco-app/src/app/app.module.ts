import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser'; 
import { AppRoutingModule } from './app.routes'; 
import { RouterModule, Routes } from '@angular/router';
import { App } from './app';


@NgModule({
  
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClient
  ],
  bootstrap: [App]
})

@NgModule({  
  exports: [RouterModule]
})
export class AppModule {}
