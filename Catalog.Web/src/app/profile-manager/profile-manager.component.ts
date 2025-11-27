import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Profile } from '../models/profile.model';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-profile-manager',
  standalone: true,
  imports: [AppModule],
  templateUrl: './profile-manager.component.html',
  styleUrls: ['./profile-manager.component.scss']
})
export class ProfileManagerComponent {
  profilesData: Profile[];
  showForm: boolean = false;
  profile: Profile = new Profile(0, '', '');

  constructor() {
    this.profilesData = [
      new Profile(1, 'f1', 'l1'),
      new Profile(2, 'f2', 'l2'),
      new Profile(3, 'f3', 'l3'),
    ];
  }

  get profiles(): Profile[] {
    return this.profilesData.sort((a, b) => a.id > b.id ? -1 : 1);
  }

  add() {
    this.showForm = true;
  }

  edit(profile: Profile) {
    this.showForm = true;
    this.profile.id = profile.id;
    this.profile.firstName = profile.firstName;
    this.profile.lastName = profile.lastName;
  }

  delete(id: number) {
    let index = this.profiles.findIndex(p => p.id == id);
    this.profiles.splice(index, 1);
  }

  cancel() {
    this.showForm = false;
  }

  save(profileForm: NgForm) {
    if (profileForm.invalid) {
      return;
    }
    let maxId = this.profilesData.sort((a, b) => a.id > b.id ? -1 : 1)[0].id;
    let newProfile = new Profile(++maxId, this.profile.firstName, this.profile.lastName);

    this.profilesData.push(newProfile);
    this.showForm = false;

    this.profile.firstName = '';
    this.profile.lastName = '';
  }
}
