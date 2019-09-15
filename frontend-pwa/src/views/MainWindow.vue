<template>
    <div class="page">
        <div class="header">
            <div class="logo">
                <span class="green">ECO</span>
                <span class="foreground-green">EYE</span>
            </div>
        </div>
        <div class="nav">
            <div class="box-shadow-filter">
                <div class="img">
                    <img class="anchor" src="~@/assets/images/bg-main.png" />
                </div>
            </div>
            <div class="menu">
                <div class="button" @click="selected = 0" :class="{ 'active': selected === 0 }">How it works?</div>
                <div class="button" @click="selected = 1; getRandomFact();" :class="{ 'active': selected === 1 }">Interesting facts</div>
                <div class="button" @click="selected = 2" :class="{ 'active': selected === 2 }">Garbage can map</div>
            </div>
        </div>
        <img src="@/assets/images/bg2.png" class="marginTop2" width="100%" />
        <div class="content">
            <div class="container">
                <template v-if="selected === 0">
                    You simply take a photo of an object, so we're able to tell you what material it is made of. Then we will inform you which garbage can you should use. Only thing you will need to do is just check a map and find a proper can!
                </template>
                <template v-if="selected === 1">
                    {{ randomFact }}
                </template>
                <template v-if="selected === 2">
                    <img class="map" src="@/assets/images/map.png" alt="">
                </template>

            </div>
        </div>
        <div class="footer">
            <div style="width: 33%;"></div>
            <div @click="$router.push('/camera')" class="circle"></div>
            <div style="width: 33%;"></div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import axios from 'axios';

    enum selectedButton {
        howItWorks,
        InterestingFacts,
        garbageCanMap,
    }

    @Component
    export default class MainWindow extends Vue {
        private randomFact: string = '';

        private selected: selectedButton = selectedButton.howItWorks;

        private async getRandomFact() {
            const response = await axios.get('https://hyapi.v-rp.eu/api/Fact/random');
            if (response.status === 200) {
                this.randomFact = response.data.content;
            }
        }

        private async created() {
            await this.getRandomFact();
        }
    }
</script>

<style scoped lang="scss">
    .map {
        width: 100%;
    }
    .center {
        text-align: center;
    }

    .box-shadow-filter {

        filter: drop-shadow(-1px 6px 3px rgba(50, 50, 0, 0.3));
    }

    .marginTop2 {
        margin-top: 2em;
    }

    .page {
        position: absolute;
        width: 100%;
        height: 100%;
        background-color: #D4F8E1;

        .header {
            width:100%;
            height: 6em;
            background-image: url("~@/assets/images/header.png");
            background-repeat: no-repeat;
            background-size: cover;

            .logo {
                position: absolute;
                width: 100%;
                text-align: center;
                top: 1em;
                letter-spacing: -1.5px;

                font-family: 'MavenPro-Bold', sans-serif;
                font-size: 1.2em;

                .green {
                    color: #15D37E;
                }

                .foreground-green {
                    background-color: #15D37E;
                    color: #FFFFFF;
                    margin-left: 0.1em;
                    padding-left: 0.1em;
                    padding-right: 0.1em;
                }
            }
        }

        .nav {
            display: flex;
            flex-direction: row;
            justify-content: space-around;
            align-items: center;
            width: 100%;
            box-sizing: border-box;
            position: relative;
            top: -0.5em;

            .img {


                top: -2em;
                left: 0.5em;

                background-color: #FFFFFF;

                width: 10em;
                height: 10em;
                clip-path: circle(50% at 50% 50%);

                .anchor {
                    position: absolute;
                    width: 9em;
                    height: 9em;
                    clip-path: circle(50% at 50% 50%);
                    left: 0.5em;
                    top: 0.5em;
                }
            }
            .menu {

                right: 1em;

                /*background-color: red;*/

                .button {
                    font-family: 'MavenPro-Bold', sans-serif;
                    display: block;
                    width: 100%;
                    text-align: center;
                    background-color: #FFFFFF;
                    border-radius: 2em;
                    padding: 0.5em 1em;

                    font-size: 1rem;
                    box-sizing: border-box;
                    margin-bottom: 0.5em;

                    transition: color, 500ms;
                    color: #181818;

                    &:hover, &.active {
                        color: #15D37E;
                    }
                }
            }
        }

        .content {
            position: relative;
            top: -4px;
            width: 100%;
            min-height: 100%;
            background-color: #BFE3CB;
            box-sizing: content-box;
            text-align: justify;

            .container {
                color: #181818;
                background-color: #AAD3B9;
                margin-left: 2em;
                margin-right: 2em;
                padding: 1em 2em;
                font-family: 'MavenPro-Regular', sans-serif;

                -webkit-box-shadow: inset 0 0 10px 4px rgba(0,0,0,0.2);
                -moz-box-shadow: inset 0 0 10px 4px rgba(0,0,0,0.2);
                box-shadow: inset 0 0 10px 4px rgba(0,0,0,0.2);

                border-radius: 0.5em;
            }
        }

        .footer {
            position: fixed;
            width: 100%;
            bottom: 0;
            height: 10vh;
            background-image: linear-gradient(rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.5));
            display: flex;
            justify-content: space-around;
            align-items: center;
            color: #FFFFFF;
            font-family: 'MavenPro-Regular', sans-serif;

            .circle {
                width: 3em;
                height: 3em;
                border-radius: 1.5em;

                background: #F2F1F2;
                opacity: 0.3;

                &:hover {
                    opacity: 0.5;
                }

                &:active {
                    opacity: 1;
                }
            }
        }
    }
</style>
