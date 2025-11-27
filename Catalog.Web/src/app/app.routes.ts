import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileManagerComponent } from './profile-manager/profile-manager.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'product-list', component: ProductListComponent },
    { path: 'profile', component: ProfileComponent },    
    { path: 'profiles', component: ProfileManagerComponent },    
]
